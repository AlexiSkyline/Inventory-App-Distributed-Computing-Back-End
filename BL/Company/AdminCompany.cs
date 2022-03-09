using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventory.API.Helpers;
using Unach.Inventory.API.Model.Request;
using Unach.Inventory.API.Model.Response;
using Unach.Inventory.API.Model;
namespace Unach.Inventory.API.BL.Company;

public class AdminCompany {
    public async Task<CompanyResponse> CreateCompany( CompanyRequest CompanyRequest ) {
        CompanyResponse results = new CompanyResponse();

        using(var connection = new SqlConnection( ContextDB.ConnectionString )) {
            connection.Open();

            var commandStoredProcedure = new SqlCommand {
                Connection  = connection,
                CommandText = "[dbo].[AdministracionEmpresas]",
                CommandType = CommandType.StoredProcedure
            };

            commandStoredProcedure.Parameters.AddWithValue( "@Nombre", CompanyRequest.Name );
            commandStoredProcedure.Parameters.AddWithValue( "@Direccion", CompanyRequest.Address );
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
}