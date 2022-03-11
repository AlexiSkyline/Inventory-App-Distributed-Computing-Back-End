using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.Product;

public class AdminProduct {
    public async Task<ProductResponse> CreateProduct( ProductRequest ProductRequest ) {
        ProductResponse results = new ProductResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionArticulos]",
                CommandType = CommandType.StoredProcedure
            };
            
            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", ProductRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", ProductRequest.Description );
            commandStoredProcedure.Parameters.AddWithValue( "@Precio", ProductRequest.Price );
            commandStoredProcedure.Parameters.AddWithValue( "@IdUnidadMedida", ProductRequest.IdUnitMesurement );
            commandStoredProcedure.Parameters.AddWithValue( "@IdMarca", ProductRequest.IdBrand );
            commandStoredProcedure.Parameters.AddWithValue( "@Stock", ProductRequest.Stock );
            commandStoredProcedure.Parameters.AddWithValue( "@IdProveedor", ProductRequest.IdProvider );
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

            var infoProduct = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProduct.Read() ) {
                results.Id             = infoProduct.GetGuid( "Id" );
                results.Name           = infoProduct.GetString( "Nombre" );
                results.Description    = infoProduct.GetString( "Descripcion" );
                results.Price          = infoProduct.GetDecimal( "Precio" );
                results.UnitMesurement = infoProduct.GetString( "UnidadMedida" );
                results.Brand          = infoProduct.GetString( "Marca" );
                results.Stock          = infoProduct.GetInt32( "Stock" );
                results.Provider       = infoProduct.GetString( "Proveedor" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> GetProducts() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionArticulos]",
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

            var infoProduct = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProduct.Read() ) {
                var FormatResult = new { 
                    Id             = infoProduct.GetGuid( "Id" ), 
                    Name           = infoProduct.GetString( "Nombre" ),
                    Description    = infoProduct.GetString( "Descripcion" ),
                    Price          = infoProduct.GetDecimal( "Precio" ),
                    UnitMesurement = infoProduct.GetString( "UnidadMedida" ),
                    Brand          = infoProduct.GetString( "Marca" ),
                    Stock          = infoProduct.GetInt32( "Stock" ),
                    Provider       = infoProduct.GetString( "Proveedor" )
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

    public async Task<ProductResponse> UpdateProduct( Guid IdProduct, ProductRequest ProductRequest ) {
        ProductResponse results = new ProductResponse();
        ProductRequest.Id       = IdProduct;

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionArticulos]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Id", ProductRequest.Id );
            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", ProductRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Descripcion", ProductRequest.Description );
            commandStoredProcedure.Parameters.AddWithValue( "@Precio", ProductRequest.Price );
            commandStoredProcedure.Parameters.AddWithValue( "@IdUnidadMedida", ProductRequest.IdUnitMesurement );
            commandStoredProcedure.Parameters.AddWithValue( "@IdMarca", ProductRequest.IdBrand );
            commandStoredProcedure.Parameters.AddWithValue( "@Stock", ProductRequest.Stock );
            commandStoredProcedure.Parameters.AddWithValue( "@IdProveedor", ProductRequest.IdProvider );
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

            var infoProduct = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProduct.Read() ) {
                results.Id             = infoProduct.GetGuid( "Id" );
                results.Name           = infoProduct.GetString( "Nombre" );
                results.Description    = infoProduct.GetString( "Descripcion" );
                results.Price          = infoProduct.GetDecimal( "Precio" );
                results.UnitMesurement = infoProduct.GetString( "UnidadMedida" );
                results.Brand          = infoProduct.GetString( "Marca" );
                results.Stock          = infoProduct.GetInt32( "Stock" );
                results.Provider       = infoProduct.GetString( "Proveedor" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }
}