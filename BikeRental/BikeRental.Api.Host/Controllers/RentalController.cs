using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Rentals;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD операций над арендами
/// </summary>
/// <param name="service">Служба приложения для CRUD операций над арендами</param>
/// <param name="logger">Логгер контроллера</param>
public class RentalController(
    IApplicationService<RentalDto, RentalCreateUpdateDto, int> service,
    ILogger<RentalController> logger)
    : CrudControllerBase<RentalDto, RentalCreateUpdateDto, int>(service, logger);