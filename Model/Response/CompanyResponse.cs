namespace Unach.Inventory.API.Model.Response;

public class CompanyResponse : SingleResponse {
    public Guid? Id { get; set; }
    public string? Name { get; set; }     
    public string? Address { get; set; }
}