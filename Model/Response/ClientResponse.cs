namespace Unach.Inventory.API.Model.Response;

public class ClientResponse : SingleResponse {
    public Guid? Id { get; set; }
    public string? Name { get; set; }     
    public string? LastName { get; set; }
    public string? RFC { get; set; } 	
    public string? Address { get; set; }  
    public string? Email { get; set; }     
    public string? PhoneNumber { get; set; }
}