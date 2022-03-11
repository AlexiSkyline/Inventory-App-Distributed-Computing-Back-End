using System;
namespace Unach.Inventory.API.Model.Response;

public class SalesResponse : SingleResponse {
    public Guid? Id { get; set; }
    public string? Date { get; set; }
    public string? Seller { get; set; }    
    public string? Client { get; set; }
    public int? Folio { get; set; }
    public string? Business { get; set; }
    public Decimal? Total { get; set; }
    public Decimal? IVA { get; set; }
    public Decimal? SubTotal { get; set; }
    public string? PaymentType { get; set; }
}