using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ClientController : ControllerBase {
    #region "Properties"
        AdminClient BLLClient = new AdminClient();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateClient( ClientRequest clientRequest ) {
            var request = await BLLClient.CreateClient( clientRequest );
            return Created( "", request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetClients() {
            var request = await BLLClient.GetClients();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateClient( Guid id, ClientRequest clientRequest ) {
            var request = await BLLClient.UpdateClient( id, clientRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<ClientRequest>> DeleteClient( Guid id ) {
            var request = await BLLClient.DeleteClient( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }

        [HttpGet( "{name}" )]
        public async Task<IActionResult> FilterClients( string name ) {
            var request = await BLLClient.FilterClients( name );        
            return Ok( request );
        }
    #endregion
}