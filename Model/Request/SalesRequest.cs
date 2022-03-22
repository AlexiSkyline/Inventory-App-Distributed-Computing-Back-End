using System.Data.SqlTypes;
using System;
using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class SalesRequest {
    public Guid? Id { get; set; }

    [Required( ErrorMessage = "The Date is required." )]     
    public DateTime? Date { get; set; }
    
    public Guid? IdSeller { get; set; }
    
    public Guid? IdClient { get; set; }

    [Required( ErrorMessage = "The Stock is required." )]
    public int? Folio { get; set; }

    public Guid? IdBusiness { get; set; }

    [Required( ErrorMessage = "The Total is required." )]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public Nullable<double> Total { get; set; }
    
    [Required( ErrorMessage = "The IVA is required." )]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public Nullable<double> IVA { get; set; }
    
    [Required( ErrorMessage = "The SubTotal is required." )]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public Nullable<double> SubTotal { get; set; }
    
    [StringLength(30)]
    [Required( ErrorMessage = "The PaymentType is required." )]     
	[MinLength( 5, ErrorMessage  = "The PaymentType must have a Minimum of 05 Characters." )]
	[MaxLength( 30, ErrorMessage = "The PaymentType must have a Maximum of 30 Characters." )]
    public string? PaymentType { get; set; }
}