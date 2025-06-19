using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController(IService<Flight> flightService, IService<Passenger> personService, IRepository<Flight> flightRepository, IMapper mapper)
    : SecureFlightBaseController(mapper)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var flights = await flightService.GetAllAsync();
        return MapResultToDataTransferObject<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }

    [HttpPut]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Put(string PassengerId, long FlightId)
    {
        var flightResult = await flightService.FindAsync(new object[] { FlightId });
        var passengerResult = await personService.FindAsync(new object[] { PassengerId });

        if (flightResult == null || !flightResult.Succeeded || passengerResult == null || !passengerResult.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var flight = flightResult.Result;
        var passenger = passengerResult.Result;

        if (flight == null || passenger == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        flight.Passengers.Add(passenger);
        flightRepository.Update(flight);
        await flightRepository.SaveChangesAsync();
        return Ok();
    }
}