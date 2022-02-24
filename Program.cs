using Microsoft.EntityFrameworkCore;
using Unach.Inventory.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// * Assign the database connection string
ContextDB.ConnectionString = builder.Configuration.GetConnectionString( "DBConexion" );
builder.Services.AddDbContext<InventoryAPIContext>(( option ) => option.UseSqlServer( ContextDB.ConnectionString ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
