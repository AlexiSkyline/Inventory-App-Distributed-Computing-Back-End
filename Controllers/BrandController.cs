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

    #region  "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateBrand( BrandRequest BrandModel ) {
            var request = await BLLBrand.CreateAndUpdateBrand( BrandModel, "Insertar" );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetBrands() {
            var request = await BLLBrand.ReadBrands();
            return Ok( request );
        }

        [HttpPut( "" )]
        public async Task<IActionResult> UpdateUnitMeasurement( BrandRequest BrandModel ) {
            var request = await BLLBrand.CreateAndUpdateBrand( BrandModel, "Actualizar" );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<BrandRequest>> DeleteBrand( Guid id ) {
            var request = await BLLBrand.DeleteBrand( id );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
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