using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class UnitMeasurementRequest {
    [Key]
    [StringLength(40)]
    [Required( ErrorMessage = "The ID is required." )]
    [MinLength( 40, ErrorMessage = "The ID must have a Minimum of 40 characters." )]
	[MaxLength( 40, ErrorMessage = "The ID must have a Maximum of 40 characters." )]
    public string? Id { get; set; }

    [StringLength(50)]
    [Required( ErrorMessage = "The Description is required." )]     
	[MinLength( 5, ErrorMessage  = "The Description must have a Minimum of 5 Characters." )]
	[MaxLength( 50, ErrorMessage = "The Description must have a Maximum of 50 Characters." )]
    public string? Description { get; set; }
}