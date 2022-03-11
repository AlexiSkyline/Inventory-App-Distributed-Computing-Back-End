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

        [HttpGet( "" )]
        public async Task<IActionResult> GetProducts() {
            var request = await BLLProduct.GetProducts();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateProduct( Guid id, ProductRequest ProductModel ) {
            var request = await BLLProduct.UpdateProduct( id, ProductModel );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<ProductRequest>> DeleteProduct( Guid id ) {
            var request = await BLLProduct.DeleteProduct( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }

        [HttpGet( "{name}" )]
        public async Task<IActionResult> FilterProducts( string name ) {
            var request = await BLLProduct.FilterProducts( name );        
            return Ok( request );
        }
    #endregion
}