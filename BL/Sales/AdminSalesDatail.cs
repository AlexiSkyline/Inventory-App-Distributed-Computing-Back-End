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

            var infoProduct = await commandStoredProcedure.ExecuteReaderAsync();

            while( infoProduct.Read() ) {
                results.Id            = infoProduct.GetGuid( "Id" );
                results.Folio         = infoProduct.GetInt32( "Folio" );
                results.Product       = infoProduct.GetString( "Articulo" );
                results.AmountProduct = infoProduct.GetInt32( "Cantidad" );
                results.PurchasePrice = infoProduct.GetDecimal( "PrecioCompra" );
                results.Amount        = infoProduct.GetDecimal( "Importe" );
                results.Date          = infoProduct.GetDateTime( "Fecha" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }
}