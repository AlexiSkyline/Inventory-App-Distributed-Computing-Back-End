using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class DescriptionRequest {
    [StringLength(50)]
    [Required( ErrorMessage = "The Description is required." )]     
	[MinLength( 5, ErrorMessage  = "The Description must have a Minimum of 05 Characters." )]
	[MaxLength( 50, ErrorMessage = "The Description must have a Maximum of 50 Characters." )]
    public string? Description { get; set; }
}