using Microsoft.EntityFrameworkCore;
using Unach.Inventory.API.Helpers;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// * Assign the database connection string
ContextDB.ConnectionString = builder.Configuration.GetConnectionString( "DBConexion" );
builder.Services.AddDbContext<InventoryAPIContext>(( option ) => option.UseSqlServer( ContextDB.ConnectionString ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors( options => {
    options.AddPolicy( name: MyAllowSpecificOrigins, policy => {
        policy.WithOrigins( "http://localhost:3000", "http://localhost:4200" ).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors( MyAllowSpecificOrigins );

app.UseAuthorization();

app.MapControllers();

app.Run();
