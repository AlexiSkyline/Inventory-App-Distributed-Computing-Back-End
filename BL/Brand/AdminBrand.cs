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

    public async Task<Object> ReadBrands() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionMarcas]",
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
                var formatResponse = new { 
                    Id = infoUnitMeasurement.GetInt32( "Id" ), 
                    Description = infoUnitMeasurement.GetString( "Descripcion" ) 
                };

                results.Add( formatResponse );
            }

            connection.Close();
            messageWarning.Status  = ( bool ) successStatus.Value;
            messageWarning.Message = ( string ) message.Value;
        }

        FormatResult FormatResult = new FormatResult();
        FormatResult.Results = results;

        if( results.Count == 0 ) {
            FormatResult.Message = "The table is empty";
            FormatResult.Status  = false;
        } else {
            FormatResult.Message = messageWarning.Message;
            FormatResult.Status  = messageWarning.Status;
        }

        return FormatResult;
    }

    public async Task<BrandResponse> DeleteBrand( int IdBrand ) {
        BrandResponse results     = new BrandResponse();
        BrandRequest BrandRequest = new BrandRequest();
        BrandRequest.Id           = IdBrand;

        if( BrandRequest.Id != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[AdministracionMarcas]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Id", BrandRequest.Id );
                commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", BrandRequest.Description );
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

                var infoUnitMeasurement = await commandStoredProcedure.ExecuteReaderAsync();

                while( infoUnitMeasurement.Read() ) {
                    results.Id          = infoUnitMeasurement.GetInt32( "Id" );
                    results.Description = infoUnitMeasurement.GetString( "Descripcion" );
                }

                connection.Close();
                results.Status  = ( bool ) successStatus.Value;
                results.Message = ( string ) message.Value; 
            }
        } else {
            results.Status  = false;
            results.Message = "The ID cannot be Empty";
        }

        return results;
    }

    public async Task<Object> FilterBrands( string description ) {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();
        BrandRequest BrandRequest  = new BrandRequest();
        BrandRequest.Description   = description;
        
        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionMarcas]",
                CommandType = CommandType.StoredProcedure
            };
            
            commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", BrandRequest.Description );
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

            var infoUnitMeasurement = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoUnitMeasurement.Read() ) {
                var formatResponse = new { 
                    Id = infoUnitMeasurement.GetInt32( "Id" ), 
                    Description = infoUnitMeasurement.GetString( "Descripcion" ) 
                };

                results.Add( formatResponse );
            }

            connection.Close();
            messageWarning.Status  = ( bool ) successStatus.Value;
            messageWarning.Message = ( string ) message.Value; 
        }

        FormatResult FormatResult = new FormatResult();
        FormatResult.Results = results;
        FormatResult.Message = messageWarning.Message;
        FormatResult.Status  = messageWarning.Status;

        return ( results.Capacity != 0 ) ? FormatResult : messageWarning;
    }
}