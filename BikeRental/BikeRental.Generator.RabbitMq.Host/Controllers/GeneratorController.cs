using BikeRental.Application.Contracts.Rentals;
using BikeRental.Generator.RabbitMq.Host.Generator;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Generator.RabbitMq.Host.Controllers;

/// <summary>
/// Generates rental contracts and publishes them to RabbitMQ in batches.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(ILogger<GeneratorController> logger, BikeRentalRabbitMqProducer producerService) : ControllerBase
{
    /// <summary>
    /// Generates <paramref name="payloadLimit"/> rental contracts, sends them to RabbitMQ in batches of
    /// <paramref name="batchSize"/> with <paramref name="waitTime"/> seconds delay between batches,
    /// and returns the generated contracts.
    /// </summary>
    /// <param name="batchSize">Contracts per batch; must be greater than 0.</param>
    /// <param name="payloadLimit">Total contracts to generate; must be greater than 0.</param>
    /// <param name="waitTime">Delay between batches in seconds; must be greater than or equal to 0.</param>
    /// <param name="cancellationToken">Token used to cancel generation/publishing.</param>
    /// <returns>List of generated <see cref="RentalCreateUpdateDto"/> items.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<RentalCreateUpdateDto>>> Get([FromQuery] int batchSize, [FromQuery] int payloadLimit, [FromQuery] int waitTime, CancellationToken cancellationToken)
    {
        if (batchSize <= 0)
            return BadRequest("batchSize must be greater than 0");

        if (payloadLimit <= 0)
            return BadRequest("payloadLimit must be greater than 0");

        if (waitTime < 0)
            return BadRequest("waitTime must be greater than or equal to 0");

        logger.LogInformation("Generating {limit} contracts via {batchSize} batches and {waitTime}s delay", payloadLimit, batchSize, waitTime);

        try
        {
            var list = new List<RentalCreateUpdateDto>(payloadLimit);
            var counter = 0;

            while (counter < payloadLimit)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var remaining = payloadLimit - counter;
                var currentBatchSize = Math.Min(batchSize, remaining);

                var batch = RentalGenerator.GenerateRentals(currentBatchSize);

                var sent = await producerService.SendAsync(batch, cancellationToken);

                if (sent)
                {
                    logger.LogInformation("Batch of {batchSize} items has been sent", batch.Count);
                }
                else
                {
                    logger.LogWarning("Batch of {batchSize} items was not sent", batch.Count);
                }

                counter += batch.Count;
                list.AddRange(batch);

                await Task.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken);
            }

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok(list);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Request was cancelled during {method} method of {controller}", nameof(Get), GetType().Name);
            return Problem(statusCode: 499, title: "Client Closed Request");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}