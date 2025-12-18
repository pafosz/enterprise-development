using BikeRental.Application.Contracts.Rentals;
using RabbitMQ.Client;
using System.Text.Json;

namespace BikeRental.Generator.RabbitMq.Host;

/// <summary>
/// RabbitMQ producer responsible for publishing batches of rental contracts
/// (<see cref="RentalCreateUpdateDto"/>) to the configured queue.
/// </summary>
public class BikeRentalRabbitMqProducer(IConfiguration configuration, IConnection rabbitMqConnection, ILogger<BikeRentalRabbitMqProducer> logger)
{
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    /// <summary>
    /// Publishes a batch of rental contracts as a single RabbitMQ message to the configured queue.
    /// </summary>
    /// <param name="batch">Contracts to publish (serialized as a JSON array).</param>
    /// <param name="cancellationToken">Token used to cancel channel creation and publish operations.</param>
    public async Task SendAsync(IList<RentalCreateUpdateDto> batch, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {queue}", batch.Count, _queueName);

            var payload = JsonSerializer.SerializeToUtf8Bytes(batch);

            await using var channel = await rabbitMqConnection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, mandatory: false, body: payload, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occurred during sending a batch of {count} contracts to {queue}", batch.Count, _queueName);
        }
    }
}