using Microsoft.AspNetCore.Mvc;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class AuthController : ControllerBase {
    #region "Properties"
        BL.Auth.Login login = new BL.Auth.Login();
    #endregion

    #region  "Methods"
        [HttpPost( "Login" )]
        public async Task<IActionResult> Login( Model.Request.LoginModel loginModel ) {
            var request = await login.LoginPortal( loginModel );

            if( request.Status == false ) {
                var message = new { request.Message, request.Status };
                return Ok( message );
            }

            return Ok( request );
        }
    #endregion
}