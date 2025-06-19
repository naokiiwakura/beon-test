using AutoMapper;
using SecureFlight.Api.Models;
using SecureFlight.Core.Entities;

namespace SecureFlight.Api.MappingProfiles;

public class DataTransferObjectsMappingProfile : Profile
{
    public DataTransferObjectsMappingProfile()
    {
        CreateMap<Airport, AirportDataTransferObject>();
        CreateMap<Flight, FlightDataTransferObject>();
        CreateMap<Passenger, PassengerDataTransferObject>();
    }
}