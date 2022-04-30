using System;
namespace Unach.Inventory.API.Model.Response;

public class SalesDetailResponse : SingleResponse {
    public Guid? Id { get; set; }
    public Guid? IdSale { get; set; }
    public int? Folio { get; set; }
    public Guid? IdProduct { get; set; }
    public string? Product { get; set; }
    public int? AmountProduct { get; set; }
    public Decimal? PurchasePrice { get; set; }
    public Decimal? Amount { get; set; }
    public DateTime? Date { get; set; }
}