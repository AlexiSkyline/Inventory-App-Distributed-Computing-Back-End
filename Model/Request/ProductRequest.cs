using System.Data.SqlTypes;
using System;
using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class ProductRequest {
    public Guid? Id { get; set; }

    [StringLength(50)]
    [Required( ErrorMessage = "The Name is required." )]     
	[MinLength( 5, ErrorMessage  = "The Name must have a minimum 05 characters" )]
	[MaxLength( 50, ErrorMessage = "The Name must have a Maximum 50 characters." )]
	public string? Name { get; set; } 

    [StringLength(50)]
    [Required( ErrorMessage = "The Description is required." )]     
	[MinLength( 3, ErrorMessage  = "The Description must have a Minimum of 03 Characters." )]
	[MaxLength( 50, ErrorMessage = "The Description must have a Maximum of 50 Characters." )]
    public string? Description { get; set; }

    [Required( ErrorMessage = "The Price is required." )]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public Nullable<double> Price { get; set; }

    public Guid? IdUnitMesurement { get; set; }

    public Guid? IdBrand { get; set; }

    [Required( ErrorMessage = "The Stock is required." )]   
    public int? Stock { get; set; }
    
    public Guid? IdProvider { get; set; }

}