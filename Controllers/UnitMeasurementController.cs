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

        [HttpGet( "" )]
        public async Task<IActionResult> ReadUnitMeasurement() {
            var request = await BLLUnitMeasurement.ReadMeasurementResponse();

            if( request.Count == 0 ) {
                var message = new { message = "The table is empty" };
                return Ok( message );
            }

            return Ok( request );
        }

        [HttpPut( "" )]
        public async Task<IActionResult> UpdateUnitMeasurement( UnitMeasurementRequest unitMeasurementRequest ) {
            var request = await BLLUnitMeasurement.UpdateUnitMeasurement( unitMeasurementRequest );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }
        
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<UnitMeasurementRequest>> DeleteUnitMeasurement( int id ) {
            var request = await BLLUnitMeasurement.DeleteUnitMeasurement( id );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }
    #endregion
}