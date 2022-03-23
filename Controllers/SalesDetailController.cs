using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Sales;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SalesDetailController : ControllerBase {
    #region "Properties"
        AdminSalesDetail BLLSalesDetail = new AdminSalesDetail();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateSalesDetail( SalesDetailRequest SalesDetailRequest ) {
            var request = await BLLSalesDetail.CreateSalesDetail( SalesDetailRequest );
            return Created( "", request );
        }
    #endregion
}