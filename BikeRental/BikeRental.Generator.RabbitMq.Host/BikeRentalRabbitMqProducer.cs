using BikeRental.Application.Contracts.Rentals;
using RabbitMQ.Client;
using System.Text.Json;

namespace BikeRental.Generator.RabbitMq.Host;

/// <summary>
/// RabbitMQ producer responsible for publishing batches of rental contracts
/// (<see cref="RentalCreateUpdateDto"/>) to the configured queue.
/// </summary>
public class BikeRentalRabbitMqProducer(
    IConfiguration configuration, 
    IConnection rabbitMqConnection, 
    ILogger<BikeRentalRabbitMqProducer> logger) : IAsyncDisposable
{
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] 
        ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    private IChannel? _channel;

    private readonly SemaphoreSlim _channelLock = new(1, 1);

    /// <summary>
    /// Returns an existing RabbitMQ channel if it was already created; otherwise creates a new channel,
    /// declares the target queue, caches the channel instance, and returns it.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel channel creation and queue declaration.</param>
    private async Task<IChannel> GetOrCreateChannelAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null)
            return _channel;

        await _channelLock.WaitAsync(cancellationToken);
        try
        {
            if (_channel is not null)
                return _channel;

            _channel = await rabbitMqConnection.CreateChannelAsync(cancellationToken: cancellationToken);

            await _channel.QueueDeclareAsync(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            return _channel;
        }
        finally
        {
            _channelLock.Release();
        }
    }

    /// <summary>
    /// Publishes a batch of rental contracts as a single RabbitMQ message to the configured queue.
    /// </summary>
    /// <param name="batch">Contracts to publish (serialized as a JSON array).</param>
    /// <param name="cancellationToken">Token used to cancel channel creation and publish operations.</param>
    /// <returns>true if the batch was published without exceptions; otherwise false.</returns>
    public async Task<bool> SendAsync(IList<RentalCreateUpdateDto> batch, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            logger.LogInformation("Sending a batch of {count} contracts to {queue}", batch.Count, _queueName);

            var payload = JsonSerializer.SerializeToUtf8Bytes(batch);

            var channel = await GetOrCreateChannelAsync(cancellationToken);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, mandatory: false, body: payload, cancellationToken: cancellationToken);

            return true;
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Sending batch was cancelled for queue {queue}", _queueName);
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occurred during sending a batch of {count} contracts to {queue}", batch.Count, _queueName);
            return false;
        }
    }

    /// <summary>
    /// Disposes the cached channel (if it was created) and releases the internal lock.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await _channelLock.WaitAsync(CancellationToken.None);
        try
        {
            if (_channel is not null)
            {
                try { await _channel.CloseAsync(CancellationToken.None); } catch { }
                await _channel.DisposeAsync();
                _channel = null;
            }
        }
        finally
        {
            _channelLock.Release();
            _channelLock.Dispose();
        }
    }
}