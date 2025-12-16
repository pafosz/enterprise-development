using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Models;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD операций над моделями велосипедов
/// </summary>
/// <param name="service">Служба приложения для CRUD операций над моделями велосипедов</param>
/// <param name="logger">Логгер контроллера</param>
public class ModelController(
    IApplicationService<ModelDto, ModelCreateUpdateDto, int> service,
    ILogger<ModelController> logger)
    : CrudControllerBase<ModelDto, ModelCreateUpdateDto, int>(service, logger);