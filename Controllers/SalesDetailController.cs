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

        [HttpGet( "" )]
        public async Task<IActionResult> GetSalesDetail() {
            var request = await BLLSalesDetail.GetSalesDatail();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateSalesDetail( Guid id, SalesDetailRequest salesDetailRequest ) {
            var request = await BLLSalesDetail.UpdateSalesDetail( id, salesDetailRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<SalesDetailRequest>> DeleteSalesDetail( Guid id ) {
            var request = await BLLSalesDetail.DeleteSalesDetail( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }

        [HttpGet( "{date}" )]
        public async Task<IActionResult> FilterSalesDetail( string date ) {
            var request = await BLLSalesDetail.FilterSalesDetail( date );        
            return Ok( request );
        }
    #endregion
}