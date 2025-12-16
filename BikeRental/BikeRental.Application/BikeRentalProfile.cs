using AutoMapper;
using BikeRental.Application.Contracts.Bicycles;
using BikeRental.Application.Contracts.Models;
using BikeRental.Application.Contracts.Rentals;
using BikeRental.Application.Contracts.Renters;
using BikeRental.Domain;

namespace BikeRental.Application;

/// <summary>
/// AutoMapper profile that defines mappings between domain entities and application DTOs.
/// </summary>
public class BikeRentalProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BikeRentalProfile"/> class
    /// and registers all mapping configurations.
    /// </summary>
    public BikeRentalProfile()
    {
        CreateMap<Renter, RenterDto>();

        CreateMap<RenterCreateUpdateDto, Renter>()
            .ForMember(d => d.Id, opt => opt.Ignore());

        CreateMap<Model, ModelDto>();

        CreateMap<ModelCreateUpdateDto, Model>()
            .ForMember(d => d.Id, opt => opt.Ignore());

        CreateMap<Bicycle, BicycleDto>();

        CreateMap<BicycleCreateUpdateDto, Bicycle>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Model, opt => opt.Ignore());

        CreateMap<Rental, RentalDto>();

        CreateMap<RentalCreateUpdateDto, Rental>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Bicycle, opt => opt.Ignore())
            .ForMember(d => d.Renter, opt => opt.Ignore())
            .ForMember(d => d.TotalPrice, opt => opt.Ignore());
    }
}
