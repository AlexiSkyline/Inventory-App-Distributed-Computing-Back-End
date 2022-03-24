using System.Data.SqlTypes;
using System;
using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class SalesDetailRequest {
    public Guid? Id { get; set; }
    
    public Guid? IdSale { get; set; }
    
    public Guid? IdProduct { get; set; }

    [Required( ErrorMessage = "The AmountProduct is required." )]
    public int? AmountProduct { get; set; }

    [Required( ErrorMessage = "The PurchasePrice is required." )]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public Nullable<double> PurchasePrice { get; set; }
    
    [Required( ErrorMessage = "The Amount is required." )]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public Nullable<double> Amount { get; set; }

    public DateTime? Date { get; set; }
}