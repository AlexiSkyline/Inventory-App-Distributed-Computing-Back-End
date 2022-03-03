using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
using Unach.Inventory.API.Model.Request;
using System.ComponentModel.DataAnnotations;
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
    #endregion
}