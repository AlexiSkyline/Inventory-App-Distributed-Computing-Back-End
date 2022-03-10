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
    #endregion
}