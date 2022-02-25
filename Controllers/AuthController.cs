using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.Auth;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;
[ApiController]
[Route( "api/[controller]" )]
public class AuthController : ControllerBase {
    #region "Properties"
        Login login = new Login();
    #endregion

    #region  "Methods"
        [HttpPost( "Login" )]
        public async Task<IActionResult> Login( LoginModel loginModel ) {
            var request = await login.Loginseller( loginModel );

            if( request.Status == false ) {
                var message = new { request.Message, request.Status };
                return Ok( message );
            }

            return Ok( request );
        }
    #endregion
}