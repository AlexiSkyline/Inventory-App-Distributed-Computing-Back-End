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
    #endregion
}