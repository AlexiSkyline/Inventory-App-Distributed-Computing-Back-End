using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SellerController :ControllerBase {
    #region "Properties"
        AdminSeller BLLSeller = new AdminSeller();
    #endregion

    #region  "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateSeller( SellerRequest sellerRequest ) {
            var request = await BLLSeller.CreateSeller( sellerRequest );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetSeller() {
            var request = await BLLSeller.ReadSeller();
            return Ok( request );
        }

        [HttpPut( "" )]
        public async Task<IActionResult> UpdateSeller( SellerRequest sellerRequest ) {
            var request = await BLLSeller.UpdateSeller( sellerRequest );

            if( request.Status == false ) {
                var FormatIdError = new {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = new {
                        Id = new string[]{ "The Email is required." }
                    }
                };
                return Ok( FormatIdError );
            }

            return Ok( request );
        }
    #endregion
}