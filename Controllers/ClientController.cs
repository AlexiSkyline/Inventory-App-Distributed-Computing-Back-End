using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ClientController : ControllerBase {
    #region "Properties"
        AdminClient BLLSeller = new AdminClient();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateClient( ClientRequest clientRequest ) {
            var request = await BLLSeller.CreateClient( clientRequest );
            return Created( "", request );
        }
    #endregion
}