using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Business;
using Unach.Inventory.API.Model.Request;

namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class BusinessController : ControllerBase {
    #region "Properties"
        AdminBusiness BLLAdminBusiness = new AdminBusiness();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateBusiness( BusinessRequest BusinessRequest ) {
            var request = await BLLAdminBusiness.CreateBusiness( BusinessRequest );
            return Created( "", request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetBusiness() {
            var request = await BLLAdminBusiness.GetBusiness();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateBusiness( Guid id, BusinessRequest BusinessModel ) {
            var request = await BLLAdminBusiness.UpdateBusiness( id, BusinessModel );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Created( "", request );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult<BusinessRequest>> DeleteBusiness( Guid id ) {
            var request = await BLLAdminBusiness.DeleteBusiness( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Unauthorized( message );
            }

            return Ok( request );
        }

        [HttpGet( "{name}" )]
        public async Task<IActionResult> FilterBusiness( string name ) {
            var request = await BLLAdminBusiness.FilterBusiness( name );        
            return Ok( request );
        }
    #endregion
}