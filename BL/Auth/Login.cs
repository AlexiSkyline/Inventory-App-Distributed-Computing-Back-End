using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
namespace Unach.Inventory.API.BL.Auth;

public class Login {
    public async Task<LoginResponse> LoginSeller( LoginRequest loginModel ) {
        LoginResponse results = new LoginResponse();

        if( loginModel.UserName != null && loginModel.Password != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[Login]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Usuario", loginModel.UserName );
                commandStoredProcedure.Parameters.AddWithValue( "@Password", loginModel.Password );

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

                var infoUser = await commandStoredProcedure.ExecuteReaderAsync();

                while( infoUser.Read() ) {
                    results.Id          = infoUser.GetGuid( "Id" );
                    results.Name        = infoUser.GetString( "Nombre" );
                    results.LastName    = infoUser.GetString( "Apellidos" );
                    results.RFC         = infoUser.GetString( "RFC" );
                    results.Email       = infoUser.GetString( "Correo" );
                    results.PhoneNumber = infoUser.GetString( "Telefono" );
                } 

                connection.Close();
                results.Status  = ( bool ) successStatus.Value;
                results.Message = ( string ) message.Value;
            }
        } else {
            results.Status  = false;
            results.Message = "The User Name and PassWord cannot be Empty";
        }

        return results;
    }
}