using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ProviderController : ControllerBase {
    #region "Properties"
        AdminProvider BLLProvider = new AdminProvider();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateProvider( ProviderRequest ProviderRequest ) {
            var request = await BLLProvider.CreateProvider( ProviderRequest );
            return Created( "", request );
        }
    #endregion
}