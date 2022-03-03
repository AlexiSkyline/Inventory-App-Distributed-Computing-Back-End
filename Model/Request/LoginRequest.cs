using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model.Request;

public class LoginRequest {
    [StringLength(20)]
    [Required( ErrorMessage = "The UserName is required." )]     
	[MinLength( 5, ErrorMessage  = "The Username must have a Minimum of 5 Characters." )]
	[MaxLength( 20, ErrorMessage = "The Username must have a Maximum of 20 Characters." )]
    public string? UserName { get; set; }

    [StringLength(60)]
    [Required( ErrorMessage = "The Password is required." )]     
	[MinLength( 6, ErrorMessage  = "The Password must have a Minimum of 6 Characters." )]
	[MaxLength( 60, ErrorMessage = "The Password must have a Maximum of 60 Characters." )]    
    public string? Password { get; set; }
}