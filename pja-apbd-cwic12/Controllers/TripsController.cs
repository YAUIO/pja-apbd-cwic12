using Microsoft.AspNetCore.Mvc;
using pja_apbd_cwic12.DTOs;
using pja_apbd_cwic12.Exceptions;
using pja_apbd_cwic12.Services;

namespace pja_apbd_cwic12.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController(IDbService service) : ControllerBase
{
    [HttpGet("{page?}")]
    public async Task<IActionResult> GetTripsAsync(int page = 1, int paginate = 10)
    {
        try
        {
            return Ok(await service.GetAllTripsAsync(page, paginate));
        }
        catch (IndexOutOfRangeException _)
        {
            return BadRequest("No such page");
        }
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> PutClientOnATripAsync(int idTrip, ClientPostDTO client)
    {
        try
        {
            await service.AssignClientToTripAsync(idTrip, client);
            return Created();
        }
        catch (ClientExistsException _)
        {
            return BadRequest("Client exists");
        }
        catch (TripAlreadyStartedException _)
        {
            return BadRequest("Trip has already started");
        }
        catch (InvalidOperationException _)
        {
            return NotFound("No such trip");
        }
    }
}