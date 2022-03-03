using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.UnitMeasurement;

public class AdminUnitMeasurement {
    public async Task<UnitMeasurementResponse> CreateUnitMeasurement( DescriptionRequest descriptionRequest ) {
        UnitMeasurementResponse results = new UnitMeasurementResponse();
        UnitMeasurementRequest unitMeasurement = new UnitMeasurementRequest();
        unitMeasurement.Description  = descriptionRequest.Description;

        if( unitMeasurement.Description != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Id", unitMeasurement.Id );
                commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", unitMeasurement.Description );
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

                var infoUnitMeasurement = await commandStoredProcedure.ExecuteReaderAsync();

                while( infoUnitMeasurement.Read() ) {
                    results.Id          = infoUnitMeasurement.GetGuid( "Id" );
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
                var FormatResult = new { 
                    Id = infoUnitMeasurement.GetGuid( "Id" ), 
                    Description = infoUnitMeasurement.GetString( "Descripcion" ) 
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

    public async Task<BrandResponse> UpdateUnitMeasurement( UnitMeasurementRequest unitMeasurementRequest ) {
        BrandResponse results = new BrandResponse();

        if( unitMeasurementRequest.Id != null && unitMeasurementRequest.Description != null ) {
            using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
                connection.Open();

                var commandStoredProcedure = new SqlCommand {
                    Connection  = connection,
                    CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                    CommandType = CommandType.StoredProcedure
                };

                commandStoredProcedure.Parameters.AddWithValue( "@Id", unitMeasurementRequest.Id );
                commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", unitMeasurementRequest.Description );
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
                    results.Id          = infoBrand.GetGuid( "Id" );
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

    public async Task<UnitMeasurementResponse> DeleteUnitMeasurement( string IdUnitMeasurement ) {
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
                    results.Id          = infoUnitMeasurement.GetGuid( "Id" );
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
                var FormatResult = new { 
                    Id = infoUnitMeasurement.GetGuid( "Id" ), 
                    Description = infoUnitMeasurement.GetString( "Descripcion" ) 
                };

                results.Add( FormatResult );
            }

            connection.Close();
            messageWarning.Status  = ( bool ) successStatus.Value;
            messageWarning.Message = ( string ) message.Value; 
        }

        FormatResponse FormatResponse = new FormatResponse();
        FormatResponse.Results = results;
        FormatResponse.Message = messageWarning.Message;
        FormatResponse.Status  = messageWarning.Status;

        return ( results.Capacity != 0 ) ? FormatResponse : messageWarning;
    }
}