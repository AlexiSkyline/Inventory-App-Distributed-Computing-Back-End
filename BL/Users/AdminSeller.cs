using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
namespace Unach.Inventory.API.BL.Users;
public class AdminSeller {
    public async Task<SellerResponse> CreateSeller( SellerRequest sellerRequest ) {
        SellerResponse results = new SellerResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionVendedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", sellerRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Apellidos", sellerRequest.LastName );
            commandStoredProcedure.Parameters.AddWithValue( "@RFC", sellerRequest.RFC );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", sellerRequest.Address );
            commandStoredProcedure.Parameters.AddWithValue( "@Correo", sellerRequest.Email );
            commandStoredProcedure.Parameters.AddWithValue( "@Telefono", sellerRequest.PhoneNumber );
            commandStoredProcedure.Parameters.AddWithValue( "@UserName", sellerRequest.UserName );
            commandStoredProcedure.Parameters.AddWithValue( "@Password", sellerRequest.Password );
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
                results.UserName    = infoBrand.GetString( "UserName" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> ReadSeller() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionVendedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Opcion", "Listar" );

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

            var infoUnitMeasurement = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoUnitMeasurement.Read() ) {
                var FormatResult = new { 
                    Id          = infoUnitMeasurement.GetGuid( "Id" ),
                    Name        = infoUnitMeasurement.GetString( "Nombre" ),
                    LastName    = infoUnitMeasurement.GetString( "Apellidos" ),
                    RFC         = infoUnitMeasurement.GetString( "RFC" ),
                    Address     = infoUnitMeasurement.GetString( "Direccion" ),
                    Email       = infoUnitMeasurement.GetString( "Correo" ),
                    PhoneNumber = infoUnitMeasurement.GetString( "Telefono" ),
                    UserName    = infoUnitMeasurement.GetString( "UserName" ),
                };

                results.Add( FormatResult );
            }

            connection.Close();
            messageWarning.Status  = ( bool ) successStatus.Value;
            messageWarning.Message = ( string ) message.Value;
        }

        FormatResponse FormatResponse = new FormatResponse();
        FormatResponse.Results = results;

        if( results.Count == 0 ) {
            FormatResponse.Message = "The table is empty";
            FormatResponse.Status  = false;
        } else {
            FormatResponse.Message = messageWarning.Message;
            FormatResponse.Status  = messageWarning.Status;
        }

        return FormatResponse;
    }

    public async Task<SellerResponse> UpdateSeller( string id, SellerRequest SellerRequest ) {
        SellerResponse results = new SellerResponse();
        SellerRequest.Id       = id;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionVendedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", SellerRequest.Id );
            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", SellerRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Apellidos", SellerRequest.LastName );
            commandStoredProcedure.Parameters.AddWithValue( "@RFC", SellerRequest.RFC );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", SellerRequest.Address );
            commandStoredProcedure.Parameters.AddWithValue( "@Correo", SellerRequest.Email );
            commandStoredProcedure.Parameters.AddWithValue( "@Telefono", SellerRequest.PhoneNumber );
            commandStoredProcedure.Parameters.AddWithValue( "@UserName", SellerRequest.UserName );
            commandStoredProcedure.Parameters.AddWithValue( "@Password", SellerRequest.Password );
            commandStoredProcedure.Parameters.AddWithValue( "@Opcion", "Actualizar" );

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
                results.Id          = infoBrand.GetGuid( "Nombre" );
                results.Name        = infoBrand.GetString( "Nombre" );
                results.LastName    = infoBrand.GetString( "Apellidos" );
                results.RFC         = infoBrand.GetString( "RFC" );
                results.Address     = infoBrand.GetString( "Direccion" );
                results.Email       = infoBrand.GetString( "Correo" );
                results.PhoneNumber = infoBrand.GetString( "Telefono" );
                results.UserName    = infoBrand.GetString( "UserName" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }
}