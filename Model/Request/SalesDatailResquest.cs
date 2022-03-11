using System.Data.SqlTypes;
using System;
using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class SalesDatailRequest {
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

    [StringLength(15)]
    [Required( ErrorMessage = "The Date is required." )]     
	[MinLength( 10, ErrorMessage = "The Date must have a Minimum of 10 Characters." )]
	[MaxLength( 15, ErrorMessage = "The Date must have a Maximum of 15 Characters." )]
    public string? Date { get; set; }
}