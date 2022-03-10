using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Product;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ProductController : ControllerBase {
    #region "Properties"
        AdminProduct BLLProduct = new AdminProduct();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateProduct( ProductRequest ProductRequest ) {
            var request = await BLLProduct.CreateProduct( ProductRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }
            
            return Created( "", request );
        }
    #endregion
}