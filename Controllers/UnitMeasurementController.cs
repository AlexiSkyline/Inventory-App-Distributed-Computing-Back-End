using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.UnitMeasurement;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class UnitMeasurementController : ControllerBase {
    #region "Properties"
        AdminUnitMeasurement BLLUnitMeasurement = new AdminUnitMeasurement();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateUnitMeasurement( UnitMeasurementRequest unitMeasurementRequest ) {
            var request = await BLLUnitMeasurement.CreateUnitMeasurement( unitMeasurementRequest );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }
    #endregion
}