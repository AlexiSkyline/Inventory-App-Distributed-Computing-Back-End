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

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateSales( Guid id, SalesRequest salesRequest ) {
            var request = await BLLSales.UpdateSales( id, salesRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<SalesRequest>> DeleteSales( Guid id ) {
            var request = await BLLSales.DeleteSales( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }

        [HttpGet( "{date}" )]
        public async Task<IActionResult> FilterSales( string date ) {
            var request = await BLLSales.FilterSales( date );        
            return Ok( request );
        }
    #endregion
}