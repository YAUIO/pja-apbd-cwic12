using Microsoft.AspNetCore.Mvc;
using pja_apbd_cwic12.Exceptions;
using pja_apbd_cwic12.Services;

namespace pja_apbd_cwic12.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(IDbService service) : ControllerBase
{
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClientAsync(int idClient)
    {
        try
        {
            await service.RemoveClientByIdAsync(idClient);
            return NoContent();
        }
        catch (ClientHasTripsException _)
        {
            return BadRequest("Can't delete a client with trips assigned to them");
        }
        catch (InvalidOperationException _)
        {
            return NotFound("No such client");
        }
    }
}