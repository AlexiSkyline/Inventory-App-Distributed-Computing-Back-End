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

            var infoSeller = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoSeller.Read() ) {
                results.Id          = infoSeller.GetGuid( "Id" );
                results.Name        = infoSeller.GetString( "Nombre" );
                results.LastName    = infoSeller.GetString( "Apellidos" );
                results.RFC         = infoSeller.GetString( "RFC" );
                results.Address     = infoSeller.GetString( "Direccion" );
                results.Email       = infoSeller.GetString( "Correo" );
                results.PhoneNumber = infoSeller.GetString( "Telefono" );
                results.UserName    = infoSeller.GetString( "UserName" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> GetSellers() {
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

            var infoSeller = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoSeller.Read() ) {
                var FormatResult = new { 
                    Id          = infoSeller.GetGuid( "Id" ),
                    Name        = infoSeller.GetString( "Nombre" ),
                    LastName    = infoSeller.GetString( "Apellidos" ),
                    RFC         = infoSeller.GetString( "RFC" ),
                    Address     = infoSeller.GetString( "Direccion" ),
                    Email       = infoSeller.GetString( "Correo" ),
                    PhoneNumber = infoSeller.GetString( "Telefono" ),
                    UserName    = infoSeller.GetString( "UserName" ),
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

    public async Task<SellerResponse> UpdateSeller( Guid id, SellerRequest SellerRequest ) {
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

            var infoSeller = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoSeller.Read() ) {
                results.Id          = infoSeller.GetGuid( "Id" );
                results.Name        = infoSeller.GetString( "Nombre" );
                results.LastName    = infoSeller.GetString( "Apellidos" );
                results.RFC         = infoSeller.GetString( "RFC" );
                results.Address     = infoSeller.GetString( "Direccion" );
                results.Email       = infoSeller.GetString( "Correo" );
                results.PhoneNumber = infoSeller.GetString( "Telefono" );
                results.UserName    = infoSeller.GetString( "UserName" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<SellerResponse> DeleteSeller( Guid IdUnit ) {
        SellerResponse results      = new SellerResponse();
        SellerRequest sellerRequest = new SellerRequest();
        sellerRequest.Id            = IdUnit;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionVendedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", sellerRequest.Id );
            commandStoredProcedure.Parameters.AddWithValue( "@Opcion", "Eliminar" );

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

            var infoSeller = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoSeller.Read() ) {
                results.Id          = infoSeller.GetGuid( "Id" );
                results.Name        = infoSeller.GetString( "Nombre" );
                results.LastName    = infoSeller.GetString( "Apellidos" );
                results.RFC         = infoSeller.GetString( "RFC" );
                results.Address     = infoSeller.GetString( "Direccion" );
                results.Email       = infoSeller.GetString( "Correo" );
                results.PhoneNumber = infoSeller.GetString( "Telefono" );
                results.UserName    = infoSeller.GetString( "UserName" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }
        
        return results;
    }
}