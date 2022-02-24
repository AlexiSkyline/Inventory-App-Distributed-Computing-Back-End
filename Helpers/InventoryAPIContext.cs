using Microsoft.EntityFrameworkCore;

namespace Unach.Inventory.API.Helpers {
    public class InventoryAPIContext : DbContext {
        public InventoryAPIContext() {}

        public InventoryAPIContext( DbContextOptions<InventoryAPIContext> options ) : base( options ) {}

        public InventoryAPIContext( String connectionString ) : 
                        base( Helpers.ContextDB.GetOptions( Helpers.ContextDB.ConnectionString )) {}
        
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            if( !optionsBuilder.IsConfigured ) {
                optionsBuilder.UseSqlServer( Helpers.ContextDB.ConnectionString, ( builder ) => {
                    builder.EnableRetryOnFailure( 5, TimeSpan.FromSeconds( 10 ), null );
                });
            }
        }
    }
}