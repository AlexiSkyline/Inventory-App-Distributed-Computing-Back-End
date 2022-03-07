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

        [HttpGet( "" )]
        public async Task<IActionResult> GetProviders() {
            var request = await BLLProvider.GetProviders();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateProvider( Guid id, ProviderRequest ProviderRequest ) {
            var request = await BLLProvider.UpdateProvider( id, ProviderRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }
    #endregion
}