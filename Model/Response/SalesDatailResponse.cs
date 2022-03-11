using System;
namespace Unach.Inventory.API.Model.Response;

public class SalesDatailResponse : SingleResponse {
    public Guid? Id { get; set; }
    public int? Folio { get; set; }
    public string? Product { get; set; }
    public int? AmountProduct { get; set; }
    public Decimal? PurchasePrice { get; set; }
    public Decimal? Amount { get; set; }
    public string? Date { get; set; }
}