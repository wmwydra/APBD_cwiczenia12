using APBD_12.Models;
using APBD_12.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_12.Controllers
{
    [ApiController]
    [Route("clients/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        
        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClientAsync(int idClient, CancellationToken cancellationToken)
        {
            try
            {
                await _clientService.DeleteClientAync(idClient, cancellationToken);
                return Ok("Client deleted");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
