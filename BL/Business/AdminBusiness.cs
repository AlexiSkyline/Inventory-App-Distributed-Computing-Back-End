using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.Business;

public class AdminBusiness {
    public async Task<BusinessResponse> CreateBusiness( BusinessRequest BusinessRequest ) {
        BusinessResponse results = new BusinessResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionEmpresas]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", BusinessRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", BusinessRequest.Address );
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

            var infoCompany = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoCompany.Read() ) {
                results.Id      = infoCompany.GetGuid( "Id" );
                results.Name    = infoCompany.GetString( "Nombre" );
                results.Address = infoCompany.GetString( "Direccion" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> GetBusiness() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionEmpresas]",
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

            var infoCompany = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoCompany.Read() ) {
                var FormatResult = new { 
                    Id      = infoCompany.GetGuid( "Id" ),
                    Name    = infoCompany.GetString( "Nombre" ),
                    Address = infoCompany.GetString( "Direccion" )
                };

                results.Add( FormatResult );
            }

            connection.Close();
            messageWarning.Status  = ( bool ) successStatus.Value;
            messageWarning.Message = ( string ) message.Value;
        }

        FormatResponse formatResponse = new FormatResponse();
        formatResponse.Results = results;
        formatResponse.Message = messageWarning.Message;
        formatResponse.Status  = messageWarning.Status;

        if( results.Count == 0 ) {
            formatResponse.Message = "The table is empty";
        }

        return formatResponse;
    }

    public async Task<BusinessResponse> UpdateBusiness( Guid IdBusiness, BusinessRequest BusinessRequest ) {
        BusinessResponse results = new BusinessResponse();
        BusinessRequest.Id       = IdBusiness;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionEmpresas]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", BusinessRequest.Id );
            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", BusinessRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", BusinessRequest.Address );
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

            var infoCompany = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoCompany.Read() ) {
                results.Id      = infoCompany.GetGuid( "Id" );
                results.Name    = infoCompany.GetString( "Nombre" );
                results.Address = infoCompany.GetString( "Direccion" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<BusinessResponse> DeleteBusiness( Guid IdBusiness ) {
        BusinessResponse results     = new BusinessResponse();
        BusinessRequest BusinessRequest = new BusinessRequest();
        BusinessRequest.Id           = IdBusiness;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionEmpresas]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", BusinessRequest.Id );
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

            var infoCompany = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoCompany.Read() ) {
                results.Id      = infoCompany.GetGuid( "Id" );
                results.Name    = infoCompany.GetString( "Nombre" );
                results.Address = infoCompany.GetString( "Direccion" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> FilterBusiness( string nameBussines ) {
        List<Object> results            = new List<Object>();
        SingleResponse messageWarning   = new SingleResponse();
        BusinessRequest BusinessRequest = new BusinessRequest();
        BusinessRequest.Name            = nameBussines;
        
        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionEmpresas]",
                CommandType = CommandType.StoredProcedure
            };
            
            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", BusinessRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Opcion", "ListaFiltrada" );

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

            var infoCompany = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoCompany.Read() ) {
                var FormatResult = new { 
                    Id      = infoCompany.GetGuid( "Id" ),
                    Name    = infoCompany.GetString( "Nombre" ),
                    Address = infoCompany.GetString( "Direccion" )
                };

                results.Add( FormatResult );
            }

            connection.Close();
            messageWarning.Status  = ( bool ) successStatus.Value;
            messageWarning.Message = ( string ) message.Value; 
        }

        FormatResponse formatResponse = new FormatResponse();
        formatResponse.Results = results;
        formatResponse.Message = messageWarning.Message;
        formatResponse.Status  = messageWarning.Status;

        return formatResponse;
    }
}