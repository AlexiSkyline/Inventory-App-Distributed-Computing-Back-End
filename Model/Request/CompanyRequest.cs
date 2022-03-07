using System.ComponentModel.DataAnnotations;

namespace Unach.Inventory.API.Model.Request;

public class CompanyRequest {
    public Guid? Id { get; set; }

    [StringLength(50)]
    [Required( ErrorMessage = "The Name is required." )]     
	[MinLength( 5, ErrorMessage  = "The Name must have a minimum 05 characters" )]
	[MaxLength( 50, ErrorMessage = "The Name must have a Maximum 50 characters." )]
    public string? Name { get; set; }    

    [StringLength(200)]
    [Required( ErrorMessage = "The Address is required." )]     
	[MinLength( 5, ErrorMessage   = "The Address must have a Minimum of 5 Characters." )]
	[MaxLength( 200, ErrorMessage = "The Address must have a Maximum of 200 Characters." )]   
    public string? Address { get; set; }
}