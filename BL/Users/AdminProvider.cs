using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
namespace Unach.Inventory.API.BL.Users;

public class AdminProvider {
    public async Task<ProviderResponse> CreateProvider( ProviderRequest ProviderRequest ) {
        ProviderResponse results = new ProviderResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionProveedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", ProviderRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Apellidos", ProviderRequest.LastName );
            commandStoredProcedure.Parameters.AddWithValue( "@RFC", ProviderRequest.RFC );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", ProviderRequest.Address );
            commandStoredProcedure.Parameters.AddWithValue( "@Correo", ProviderRequest.Email );
            commandStoredProcedure.Parameters.AddWithValue( "@Telefono", ProviderRequest.PhoneNumber );
            commandStoredProcedure.Parameters.AddWithValue( "@Opcion", "Insertar" );

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
                results.Id          = infoBrand.GetGuid( "Id" );
                results.Name        = infoBrand.GetString( "Nombre" );
                results.LastName    = infoBrand.GetString( "Apellidos" );
                results.RFC         = infoBrand.GetString( "RFC" );
                results.Address     = infoBrand.GetString( "Direccion" );
                results.Email       = infoBrand.GetString( "Correo" );
                results.PhoneNumber = infoBrand.GetString( "Telefono" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }
}