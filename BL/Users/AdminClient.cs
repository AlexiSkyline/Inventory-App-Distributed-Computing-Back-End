using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
namespace Unach.Inventory.API.BL.Users;

public class AdminClient {
    public async Task<ClientResponse> CreateClient( ClientRequest clientRequest ) {
        ClientResponse results = new ClientResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionClientes]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", clientRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Apellidos", clientRequest.LastName );
            commandStoredProcedure.Parameters.AddWithValue( "@RFC", clientRequest.RFC );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", clientRequest.Address );
            commandStoredProcedure.Parameters.AddWithValue( "@Correo", clientRequest.Email );
            commandStoredProcedure.Parameters.AddWithValue( "@Telefono", clientRequest.PhoneNumber );
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