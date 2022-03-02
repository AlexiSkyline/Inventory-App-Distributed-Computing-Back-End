namespace Unach.Inventory.API.Model.Response;

public class BrandResponse : SingleResponse {
    public Guid? Id { get; set; }
    public string? Description { get; set; }
}