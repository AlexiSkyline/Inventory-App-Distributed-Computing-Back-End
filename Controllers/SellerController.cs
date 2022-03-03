using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Users;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SellerController :ControllerBase {
    #region "Properties"
        AdminSeller BLLSeller = new AdminSeller();
    #endregion

    #region  "Methods"
        [HttpGet( "" )]
        public async Task<IActionResult> GetSeller() {
            var request = await BLLSeller.ReadSeller();
            return Ok( request );
        }
    #endregion
}