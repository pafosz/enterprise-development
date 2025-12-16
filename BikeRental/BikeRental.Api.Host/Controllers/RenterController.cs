using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD операций над арендаторами
/// </summary>
/// <param name="service">Служба приложения для CRUD операций над арендаторами</param>
/// <param name="logger">Логгер контроллера</param>
public class RenterController(
    IApplicationService<RenterDto, RenterCreateUpdateDto, int> service,
    ILogger<RenterController> logger)
    : CrudControllerBase<RenterDto, RenterCreateUpdateDto, int>(service, logger);