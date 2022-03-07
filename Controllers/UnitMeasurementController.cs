using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.UnitMeasurement;
using Unach.Inventory.API.Model.Request;
namespace Unach.Inventory.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class UnitMeasurementController : ControllerBase {
    #region "Properties"
        AdminUnitMeasurement BLLUnitMeasurement = new AdminUnitMeasurement();
    #endregion

    #region "Methods"
        [HttpPost( "" )]
        public async Task<IActionResult> CreateUnitMeasurement( UnitMeasurementRequest unitMeasurement ) {
            var request = await BLLUnitMeasurement.CreateUnitMeasurement( unitMeasurement );
            return Created( "", request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> GetUnitMeasurements() {
            var request = await BLLUnitMeasurement.GetUnitMeasurements();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateUnitMeasurement( Guid id, UnitMeasurementRequest unitMeasurementRequest ) {
            var request = await BLLUnitMeasurement.UpdateUnitMeasurement( id, unitMeasurementRequest );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Ok( message );
            }

            return Created( "", request );
        }
        
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<UnitMeasurementRequest>> DeleteUnitMeasurement( Guid id ) {
            var request = await BLLUnitMeasurement.DeleteUnitMeasurement( id );

            if( request.Status == false ) {
                var message = new { request.Message, status = 401 };
                return Ok( message );
            }

            return Ok( request );
        }

        [HttpGet( "{description}" )]
        public async Task<IActionResult> FilterUnitMeasurement( string description ) {
            var request = await BLLUnitMeasurement.FilterUnitMeasurement( description );        
            return Ok( request );
        }
    #endregion
}