using Microsoft.EntityFrameworkCore;
namespace Unach.Inventory.API.Helpers;

public class ContextDB {
    // * Get or Set the Database connection string
    public static string? ConnectionString { get; set; }
    public static DbContextOptions GetOptions( string connection ) {
        return SqlServerDbContextOptionsExtensions
                        .UseSqlServer( new DbContextOptionsBuilder(), connection ).Options;
    }
}