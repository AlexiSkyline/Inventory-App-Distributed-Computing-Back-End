using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class UnitMeasurementRequest {
    [Key]
    [StringLength(36)]
    [Required( ErrorMessage = "The ID is required." )]
    [MinLength( 36, ErrorMessage = "The ID must have a Minimum of 36 characters." )]
	[MaxLength( 36, ErrorMessage = "The ID must have a Maximum of 36 characters." )]
    public string? Id { get; set; }

    [StringLength(50)]
    [Required( ErrorMessage = "The Description is required." )]     
	[MinLength( 3, ErrorMessage  = "The Description must have a Minimum of 03 Characters." )]
	[MaxLength( 50, ErrorMessage = "The Description must have a Maximum of 50 Characters." )]
    public string? Description { get; set; }
}