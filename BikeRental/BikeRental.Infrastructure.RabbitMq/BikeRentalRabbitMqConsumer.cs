using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Rentals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace BikeRental.Infrastructure.RabbitMq;

/// <summary>
/// RabbitMQ consumer (hosted background service) that listens to a configured queue and processes
/// incoming rental contracts by creating rentals through the application service layer.
/// </summary>
public class BikeRentalRabbitMqConsumer(IConnection connection, IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<BikeRentalRabbitMqConsumer> logger) : BackgroundService
{
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    /// <summary>
    /// Establishes an AMQP channel, declares the queue, and starts consuming messages
    /// until the host requests cancellation.
    /// </summary>
    /// <param name="stoppingToken">Token used to stop listening and shut down gracefully.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Establishing channel to queue {queue}", _queueName);

        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        try
        {
            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken: stoppingToken);

            logger.LogInformation("Began listening to queue {queue}", _queueName);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (_, ea) => await ReceiveMessage(ea, stoppingToken);

            await channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        finally
        {
            try
            {
                await channel.CloseAsync(cancellationToken: CancellationToken.None);
            }
            catch
            {

            }

            await channel.DisposeAsync();
        }
    }

    /// <summary>
    /// Handles a single RabbitMQ delivery: deserializes the message body into contracts and
    /// creates rentals through <see cref="IApplicationService{TDto, TCreateUpdateDto, TKey}"/>.
    /// </summary>
    /// <param name="args">RabbitMQ delivery arguments containing the message payload.</param>
    /// <param name="stoppingToken">Cancellation token propagated from the hosted service.</param>
    private async Task ReceiveMessage(BasicDeliverEventArgs args, CancellationToken stoppingToken)
    {
        logger.LogInformation("Received a message from queue {queue}", _queueName);

        try
        {
            stoppingToken.ThrowIfCancellationRequested();

            var contracts = JsonSerializer.Deserialize<List<RentalCreateUpdateDto>>(args.Body.Span)
                ?? throw new FormatException("Unable to parse contracts from message body");

            using var scope = scopeFactory.CreateScope();
            var rentalService = scope.ServiceProvider.GetRequiredService<IApplicationService<RentalDto, RentalCreateUpdateDto, int>>();

            foreach (var contract in contracts)
            {
                try
                {
                    await rentalService.Create(contract);
                }
                catch (KeyNotFoundException ex)
                {
                    logger.LogWarning(ex, "Skipping contract due to missing related entity in {queue} with BicycleId {bicycleId} and RenterId {renterId}", _queueName, contract.BicycleId, contract.RenterId);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during receiving contracts from {queue}", _queueName);
        }
    }
}