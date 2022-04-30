using System;
namespace Unach.Inventory.API.Model.Response;

public class ProductResponse : SingleResponse {
    public Guid? Id { get; set; }
	public string? Name { get; set; } 
    public string? Description { get; set; }
    public Decimal? Price { get; set; }
    public Guid? IdUnitMesurement { get; set; }
    public string? UnitMesurement { get; set; }
    public Guid? IdBrand { get; set; }
    public string? Brand { get; set; }  
    public int? Stock { get; set; }
    public Guid? IdProvider { get; set; }
    public string? Provider { get; set; }
}