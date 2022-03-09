using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Company;
using Unach.Inventory.API.Model.Request;

namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class CompanyController : ControllerBase {
    #region "Properties"
        AdminCompany BLLAdminCompany = new AdminCompany();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateCompany( CompanyRequest companyRequest ) {
            var request = await BLLAdminCompany.CreateCompany( companyRequest );
            return Created( "", request );
        }
    #endregion
}