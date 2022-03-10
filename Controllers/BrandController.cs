using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Brand;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class BrandController : ControllerBase {
    #region "Properties"
        AdminBrand BLLBrand = new AdminBrand();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateBrand( BrandRequest BrandRequest ) {
            var request = await BLLBrand.CreateBrand( BrandRequest );
            return Created( "", request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetBrands() {
            var request = await BLLBrand.GetBrands();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateBrand( Guid id, BrandRequest BrandModel ) {
            var request = await BLLBrand.UpdateBrand( id, BrandModel );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<BrandRequest>> DeleteBrand( Guid id ) {
            var request = await BLLBrand.DeleteBrand( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }

        [HttpGet( "{description}" )]
        public async Task<IActionResult> FilterBrands( string description ) {
            var request = await BLLBrand.FilterBrands( description );        
            return Ok( request );
        }
    #endregion
}