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

            var infoProvider = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProvider.Read() ) {
                results.Id          = infoProvider.GetGuid( "Id" );
                results.Name        = infoProvider.GetString( "Nombre" );
                results.LastName    = infoProvider.GetString( "Apellidos" );
                results.RFC         = infoProvider.GetString( "RFC" );
                results.Address     = infoProvider.GetString( "Direccion" );
                results.Email       = infoProvider.GetString( "Correo" );
                results.PhoneNumber = infoProvider.GetString( "Telefono" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

     public async Task<Object> GetProviders() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionProveedores]",
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

            var infoProvider = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProvider.Read() ) {
                var FormatResult = new { 
                    Id          = infoProvider.GetGuid( "Id" ),
                    Name        = infoProvider.GetString( "Nombre" ),
                    LastName    = infoProvider.GetString( "Apellidos" ),
                    RFC         = infoProvider.GetString( "RFC" ),
                    Address     = infoProvider.GetString( "Direccion" ),
                    Email       = infoProvider.GetString( "Correo" ),
                    PhoneNumber = infoProvider.GetString( "Telefono" )
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

    public async Task<ProviderResponse> UpdateProvider( Guid id, ProviderRequest ProviderRequest ) {
        ProviderResponse results = new ProviderResponse();
        ProviderRequest.Id       = id;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionProveedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", ProviderRequest.Id );
            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", ProviderRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Apellidos", ProviderRequest.LastName );
            commandStoredProcedure.Parameters.AddWithValue( "@RFC", ProviderRequest.RFC );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", ProviderRequest.Address );
            commandStoredProcedure.Parameters.AddWithValue( "@Correo", ProviderRequest.Email );
            commandStoredProcedure.Parameters.AddWithValue( "@Telefono", ProviderRequest.PhoneNumber );
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

            var infoProvider = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProvider.Read() ) {
                results.Id          = infoProvider.GetGuid( "Id" );
                results.Name        = infoProvider.GetString( "Nombre" );
                results.LastName    = infoProvider.GetString( "Apellidos" );
                results.RFC         = infoProvider.GetString( "RFC" );
                results.Address     = infoProvider.GetString( "Direccion" );
                results.Email       = infoProvider.GetString( "Correo" );
                results.PhoneNumber = infoProvider.GetString( "Telefono" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<ProviderResponse> DeleteProvider( Guid IdUnit ) {
        ProviderResponse results      = new ProviderResponse();
        ProviderRequest ProviderRequest = new ProviderRequest();
        ProviderRequest.Id            = IdUnit;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionProveedores]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", ProviderRequest.Id );
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

            var infoClient = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoClient.Read() ) {
                results.Id          = infoClient.GetGuid( "Id" );
                results.Name        = infoClient.GetString( "Nombre" );
                results.LastName    = infoClient.GetString( "Apellidos" );
                results.RFC         = infoClient.GetString( "RFC" );
                results.Address     = infoClient.GetString( "Direccion" );
                results.Email       = infoClient.GetString( "Correo" );
                results.PhoneNumber = infoClient.GetString( "Telefono" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }
        
        return results;
    }
}