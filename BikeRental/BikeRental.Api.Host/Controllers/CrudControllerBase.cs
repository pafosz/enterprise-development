using BikeRental.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Базовый CRUD контроллер для стандартных операций над DTO
/// </summary>
/// <typeparam name="TDto">DTO для операций чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для операций создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
/// <param name="appService">Служба приложения для CRUD операций</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новый объект
    /// </summary>
    /// <param name="newDto">DTO для создания</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto newDto)
    {
        try
        {
            var result = await appService.Create(newDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Некорректная операция при создании");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании объекта");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="newDto">DTO для обновления</param>
    /// <returns>Обновлённый объект</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Edit([FromRoute] TKey id, [FromBody] TCreateUpdateDto newDto)
    {
        try
        {
            var result = await appService.Update(newDto, id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Объект не найден при обновлении");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении объекта");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] TKey id)
    {
        try
        {
            var deleted = await appService.Delete(id);
            return deleted ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении объекта");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает список объектов
    /// </summary>
    /// <returns>Список объектов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var result = await appService.GetAll();
            return result.Count == 0 ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка объектов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Возвращает объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Объект</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Get([FromRoute] TKey id)
    {
        try
        {
            var result = await appService.Get(id);
            return result is null
                ? NotFound($"Объект с идентификатором {id} не найден")
                : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении объекта");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}