using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.UnitMeasurement;

public class AdminUnitMeasurement {
    public async Task<UnitMeasurementResponse> CreateAndUpdateUnitMeasurement( UnitMeasurementRequest unitMeasurement, string Opction ) {
        UnitMeasurementResponse results = new UnitMeasurementResponse();

        if( unitMeasurement.Id != null && unitMeasurement.Description != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Id", unitMeasurement.Id );
                commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", unitMeasurement.Description );
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
            results.Message = "The ID and Description cannot be Empty";
        }

        return results;
    }

    public async Task<Object> ReadUnitMeasurement() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionUnidadesMedidas]",
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

    public async Task<UnitMeasurementResponse> DeleteUnitMeasurement( int IdUnitMeasurement ) {
        UnitMeasurementResponse results = new UnitMeasurementResponse();
        UnitMeasurementRequest unitMeasurement = new UnitMeasurementRequest();
        unitMeasurement.Id = IdUnitMeasurement;

        if( unitMeasurement.Id != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Id", unitMeasurement.Id );
                commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", unitMeasurement.Description );
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

    public async Task<Object> FilterUnitMeasurement( string description ) {
        List<Object> results                   = new List<Object>();
        SingleResponse messageWarning          = new SingleResponse();
        UnitMeasurementRequest unitMeasurement = new UnitMeasurementRequest();
        unitMeasurement.Description            = description;
        
        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                CommandType = CommandType.StoredProcedure
            };
            
            commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", unitMeasurement.Description );
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