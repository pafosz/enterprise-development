using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts.Bicycles;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер аналитических запросов, предоставляет агрегированные отчёты и выборки для домена проката велосипедов
/// </summary>
/// <param name="analyticsService">Сервис аналитики</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public sealed class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает информацию обо всех спортивных велосипедах
    /// </summary>
    /// <returns>Список велосипедов спортивного типа</returns>
    [HttpGet("sports-bicycles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BicycleDto>>> GetAllSportsBicycles()
    {
        try
        {
            var result = await analyticsService.GetAllSportsBicycles();
            return result.Count == 0 ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка спортивных велосипедов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает топ моделей велосипедов по прибыли от аренды
    /// </summary>
    /// <param name="count">Количество элементов в результате (по умолчанию 5)</param>
    /// <returns>Список моделей, отсортированных по суммарной прибыли</returns>
    [HttpGet("top-models-by-profit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TopModelByProfitDto>>> GetTopModelsByProfit([FromQuery] int count = 5)
    {
        if (count <= 0)
            return BadRequest("Параметр count должен быть больше 0");

        try
        {
            var result = await analyticsService.GetTopModelsByProfit(count);
            return result.Count == 0 ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа моделей по прибыли");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает топ моделей велосипедов по длительности аренды
    /// </summary>
    /// <param name="count">Количество элементов в результате (по умолчанию 5)</param>
    /// <returns>Список моделей, отсортированных по суммарной длительности аренды</returns>
    [HttpGet("top-models-by-duration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TopModelByDurationDto>>> GetTopModelsByDuration([FromQuery] int count = 5)
    {
        if (count <= 0)
            return BadRequest("Параметр count должен быть больше 0");

        try
        {
            var result = await analyticsService.GetTopModelsByDuration(count);
            return result.Count == 0 ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа моделей по длительности аренды");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает информацию о минимальном, максимальном и среднем времени аренды
    /// </summary>
    /// <returns>Статистика длительности аренды</returns>
    [HttpGet("rental-duration-stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetRentalDurationStats()
    {
        try
        {
            var result = await analyticsService.GetRentalDurationStats();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении статистики длительности аренды");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает суммарное время аренды велосипедов каждого типа
    /// </summary>
    /// <returns>Список значений суммарной длительности аренды, сгруппированных по типу</returns>
    [HttpGet("rental-duration-by-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TotalRentalDurationByTypeDto>>> GetTotalRentalDurationByType()
    {
        try
        {
            var result = await analyticsService.GetTotalRentalDurationByType();
            return result.Count == 0 ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении суммарной длительности аренды по типам");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает информацию о клиентах, бравших велосипеды в прокат больше всего раз
    /// </summary>
    /// <param name="count">Количество элементов в результате (по умолчанию 5)</param>
    /// <returns>Список клиентов, отсортированных по количеству аренд</returns>
    [HttpGet("top-renters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TopRenterByRentalsCountDto>>> GetTopRentersByRentalsCount([FromQuery] int count = 5)
    {
        if (count <= 0)
            return BadRequest("Параметр count должен быть больше 0");

        try
        {
            var result = await analyticsService.GetTopRentersByRentalsCount(count);
            return result.Count == 0 ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа клиентов по количеству аренд");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}