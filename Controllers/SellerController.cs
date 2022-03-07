using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SellerController : ControllerBase {
    #region "Properties"
        AdminSeller BLLSeller = new AdminSeller();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateSeller( SellerRequest sellerRequest ) {
            var request = await BLLSeller.CreateSeller( sellerRequest );
            return Created( "", request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetSeller() {
            var request = await BLLSeller.ReadSeller();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateSeller( Guid id, SellerRequest sellerRequest ) {
            var request = await BLLSeller.UpdateSeller( id, sellerRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<BrandRequest>> DeleteSeller( Guid id ) {
            var request = await BLLSeller.DeleteSeller( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }
    #endregion
}