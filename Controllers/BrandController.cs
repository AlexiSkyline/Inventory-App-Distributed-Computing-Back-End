using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Brand;
using Unach.Inventory.API.Model;
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
        public async Task<IActionResult> CreateBrand( DescriptionRequest description ) {
            var request = await BLLBrand.CreateBrand( description );
            return Ok( request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetBrands() {
            var request = await BLLBrand.ReadBrands();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateUnitMeasurement( string id, BrandRequest BrandModel ) {
            ValidateID validateID = new ValidateID();
            
            if( !validateID.IsValid(id) ) {
                return Ok( validateID.Message );
            }

            var request = await BLLBrand.UpdateBrand( id, BrandModel );
            return Ok( request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<BrandRequest>> DeleteBrand( string id ) {
            ValidateID validateID = new ValidateID();
            
            if( !validateID.IsValid(id) ) {
                return Ok( validateID.Message );
            }
            
            var request = await BLLBrand.DeleteBrand( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
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