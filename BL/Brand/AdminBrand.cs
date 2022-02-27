using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.Brand;

public class AdminBrand {
    public async Task<BrandResponse> CreateAndUpdateBrand( BrandRequest Brand, string Opction ) {
        BrandResponse results = new BrandResponse();

        if( Brand.Id != null && Brand.Description != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[AdministracionMarcas]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Id", Brand.Id );
                commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", Brand.Description );
                commandStoredProcedure.Parameters.AddWithValue( "@Opcion", Opction );

                SqlParameter successStatus  = new SqlParameter();
                successStatus.ParameterName = "@Exito";
                successStatus.SqlDbType     = SqlDbType.Bit;
                successStatus.Direction     = ParameterDirection.Output;

                commandStoredProcedure.Parameters.Add( successStatus );

                SqlParameter message  = new SqlParameter();
                message.ParameterName = "@Mensaje";
                message.SqlDbType     = SqlDbType.VarChar;
                message.Direction     = ParameterDirection.Output;
                message.Size          = 4000;

                commandStoredProcedure.Parameters.Add( message );

                var infoBrand = await commandStoredProcedure.ExecuteReaderAsync();

                while( infoBrand.Read() ) {
                    results.Id          = infoBrand.GetInt32( "Id" );
                    results.Description = infoBrand.GetString( "Descripcion" );
                }

                connection.Close();
                results.Status  = ( bool ) successStatus.Value;
                results.Message = ( string ) message.Value; 
            }
        } else {
            results.Status  = false;
            results.Message = "The ID and Description cannot be Empty";
        }

        return results;
    }
}