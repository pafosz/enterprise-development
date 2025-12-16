using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bicycles;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD операций над велосипедами
/// </summary>
/// <param name="service">Служба приложения для CRUD операций над велосипедами</param>
/// <param name="logger">Логгер контроллера</param>
public class BicycleController(
    IApplicationService<BicycleDto, BicycleCreateUpdateDto, int> service,
    ILogger<BicycleController> logger)
    : CrudControllerBase<BicycleDto, BicycleCreateUpdateDto, int>(service, logger);