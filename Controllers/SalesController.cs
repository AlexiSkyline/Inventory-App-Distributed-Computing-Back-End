using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Sales;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SalesController : ControllerBase {
    #region "Properties"
        AdminSales BLLSales = new AdminSales();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateSales( SalesRequest SalesRequest ) {
            var request = await BLLSales.CreateSales( SalesRequest );
            return Created( "", request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetSales() {
            var request = await BLLSales.GetSales();
            return Ok( request );
        }
    #endregion
}