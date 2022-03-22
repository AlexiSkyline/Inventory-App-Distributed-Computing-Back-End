using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.Sales;

public class AdminSales {
    public async Task<SalesResponse> CreateSales( SalesRequest salesRequest ) {
        SalesResponse results = new SalesResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionVentas]",
                CommandType = CommandType.StoredProcedure
            };
            
            commandStoredProcedure.Parameters.AddWithValue( "@Fecha", salesRequest.Date );
            commandStoredProcedure.Parameters.AddWithValue( "@IdVendedor", salesRequest.IdSeller );
            commandStoredProcedure.Parameters.AddWithValue( "@IdCliente", salesRequest.IdClient );
            commandStoredProcedure.Parameters.AddWithValue( "@Folio", salesRequest.Folio );
            commandStoredProcedure.Parameters.AddWithValue( "@IdEmpresa", salesRequest.IdBusiness );
            commandStoredProcedure.Parameters.AddWithValue( "@Total", salesRequest.Total );
            commandStoredProcedure.Parameters.AddWithValue( "@Iva", salesRequest.IVA );
            commandStoredProcedure.Parameters.AddWithValue( "@SubTotal", salesRequest.SubTotal );
            commandStoredProcedure.Parameters.AddWithValue( "@PagoCon", salesRequest.PaymentType );
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
                results.Id       = infoProduct.GetGuid( "Id" );
                results.Date     = infoProduct.GetDateTime( "Fecha" );
                results.Seller   = infoProduct.GetString( "Vendedor" );
                results.Client   = infoProduct.GetString( "Cliente" );
                results.Folio    = infoProduct.GetInt32( "Folio" );
                results.Business = infoProduct.GetString( "Empresa" );
                results.Total    = infoProduct.GetDecimal( "Total" );
                results.IVA      = infoProduct.GetDecimal( "Iva" );
                results.SubTotal = infoProduct.GetDecimal( "SubTotal" );
                results.PaymentType  = infoProduct.GetString( "PagoCon" );
            }

            connection.Close();
            results.Status  = ( bool ) successStatus.Value;
            results.Message = ( string ) message.Value; 
        }

        return results;
    }

    public async Task<Object> GetSales() {
        List<Object> results          = new List<Object>();
        SingleResponse messageWarning = new SingleResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionVentas]",
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
                    Id           = infoProduct.GetGuid( "Id" ),
                    Date         = infoProduct.GetDateTime( "Fecha" ),
                    Seller       = infoProduct.GetString( "Vendedor" ),
                    Client       = infoProduct.GetString( "Cliente" ),
                    Folio        = infoProduct.GetInt32( "Folio" ),
                    Business     = infoProduct.GetString( "Empresa" ),
                    Total        = infoProduct.GetDecimal( "Total" ),
                    IVA          = infoProduct.GetDecimal( "Iva" ),
                    SubTotal     = infoProduct.GetDecimal( "SubTotal" ),
                    PaymentType  = infoProduct.GetString( "PagoCon" ),
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