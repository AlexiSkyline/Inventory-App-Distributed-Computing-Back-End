using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.Sales;

public class AdminSalesDetail {
    public async Task<SalesDetailResponse> CreateSalesDetail( SalesDetailRequest salesDetailRequest ) {
        SalesDetailResponse results = new SalesDetailResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionDetalleVenta]",
                CommandType = CommandType.StoredProcedure
            };
             
            commandStoredProcedure.Parameters.AddWithValue( "@Id", salesDetailRequest.Date );
            commandStoredProcedure.Parameters.AddWithValue( "@IdVenta", salesDetailRequest.IdSale );
            commandStoredProcedure.Parameters.AddWithValue( "@IdArticulo", salesDetailRequest.IdProduct );
            commandStoredProcedure.Parameters.AddWithValue( "@Cantidad", salesDetailRequest.AmountProduct );
            commandStoredProcedure.Parameters.AddWithValue( "@PrecioCompra", salesDetailRequest.PurchasePrice );
            commandStoredProcedure.Parameters.AddWithValue( "@Importe", salesDetailRequest.Amount );
            commandStoredProcedure.Parameters.AddWithValue( "@Fecha", salesDetailRequest.Date );
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

            var infoSalesDetail = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoSalesDetail.Read() ) {
                results.Id            = infoSalesDetail.GetGuid( "Id" );
                results.Folio         = infoSalesDetail.GetInt32( "Folio" );
                results.Product       = infoSalesDetail.GetString( "Articulo" );
                results.AmountProduct = infoSalesDetail.GetInt32( "Cantidad" );
                results.PurchasePrice = infoSalesDetail.GetDecimal( "PrecioCompra" );
                results.Amount        = infoSalesDetail.GetDecimal( "Importe" );
                results.Date          = infoSalesDetail.GetDateTime( "Fecha" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> GetSalesDatail() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionDetalleVenta]",
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

            var infoSalesDetail = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoSalesDetail.Read() ) {
                var FormatResult = new { 
                    Id            = infoSalesDetail.GetGuid( "Id" ),
                    Folio         = infoSalesDetail.GetInt32( "Folio" ),
                    Product       = infoSalesDetail.GetString( "Articulo" ),
                    AmountProduct = infoSalesDetail.GetInt32( "Cantidad" ),
                    PurchasePrice = infoSalesDetail.GetDecimal( "PrecioCompra" ),
                    Amount        = infoSalesDetail.GetDecimal( "Importe" ),
                    Date          = infoSalesDetail.GetDateTime( "Fecha" )
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
}