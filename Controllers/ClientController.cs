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
            var request = await BLLClient.ReadSeller();
            return Ok( request );
        }
    #endregion
}