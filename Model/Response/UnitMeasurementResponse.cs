namespace Unach.Inventory.API.Model.Response;

public class UnitMeasurementResponse : SingleResponse {
    public Guid? Id { get; set; }
    public string? Description { get; set; }
}