using Microsoft.AspNetCore.Mvc;
using Unach.Inventory.API.BL.UnitMeasurement;
using Unach.Inventory.API.Model;
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
            return Ok( request );
        }

        [HttpGet( "" )]
        public async Task<IActionResult> ReadUnitMeasurement() {
            var request = await BLLUnitMeasurement.ReadUnitMeasurement();
            return Ok( request );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateUnitMeasurement( string id, UnitMeasurementRequest unitMeasurementRequest ) {
            ValidateID validateID = new ValidateID();
            
            if( !validateID.IsValid(id) ) {
                return Ok( validateID.Message );
            }
            
            var request = await BLLUnitMeasurement.UpdateUnitMeasurement( id, unitMeasurementRequest );

            if( request.Status == false ) {
                var message = new { request.Message };
                return Ok( message );
            }

            return Ok( request );
        }
        
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<UnitMeasurementRequest>> DeleteUnitMeasurement( string id ) {
            ValidateID validateID = new ValidateID();
            
            if( !validateID.IsValid(id) ) {
                return Ok( validateID.Message );
            }
            
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