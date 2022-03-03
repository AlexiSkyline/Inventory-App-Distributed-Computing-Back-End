using System.ComponentModel.DataAnnotations;
namespace Unach.Inventory.API.Model;

public class GeneralInformation {
	[Key]
    [StringLength(36)]
    [MinLength( 36, ErrorMessage = "The ID must have a Minimum of 36 characters." )]
	[MaxLength( 36, ErrorMessage = "The ID must have a Maximum of 36 characters." )]
    public string? Id { get; set; }
	    
    [StringLength(50)]
    [Required( ErrorMessage = "The Name is required." )]     
	[MinLength( 5, ErrorMessage  = "The Name must have a minimum 05 characters" )]
	[MaxLength( 50, ErrorMessage = "The Name must have a Maximum 50 characters." )]
	public string? Name { get; set; }     

	[StringLength(100)]
    [Required( ErrorMessage = "The Last Name is required." )]     
	[MinLength( 5, ErrorMessage   = "The Last Name must have a minimum of 05 characters." )]
	[MaxLength( 100, ErrorMessage = "The Last Name must have a Maximum of 100 Characters." )]
	public string? LastName { get; set; }

	[StringLength(13)]
    [Required( ErrorMessage = "The RFC is required." )]     
	[MinLength( 13, ErrorMessage = "El RFC debe ser Mínimo de 13 Caracteres." )]
	[MaxLength( 13, ErrorMessage = "El RFC debe ser Máximo de 13 Caracteres." )]
	public string? RFC { get; set; } 	

	[StringLength(200)]
    [Required( ErrorMessage = "The Address is required." )]     
	[MinLength( 5, ErrorMessage   = "The Address must have a Minimum of 5 Characters." )]
	[MaxLength( 200, ErrorMessage = "The Address must have a Maximum of 200 Characters." )]    
	public string? Address { get; set; }  

	[StringLength(50)]
    [Required( ErrorMessage = "The Email is required." )]     
	[MinLength( 10, ErrorMessage = "The Email must have a minimum of 10 characters." )]
	[MaxLength( 50, ErrorMessage = "The Email must have a Maximum of 50 Characters." )]
	[DataType( DataType.EmailAddress )]
	[EmailAddress]
	public string? Email { get; set; }     

	[StringLength(10)]
    [Required( ErrorMessage = "The Phone Number is required." )]     
	[MinLength( 10, ErrorMessage = "The Phone Number must have a Minimum of 10 Characters." )]
	[MaxLength( 10, ErrorMessage = "The Phone Number must have a Maximum of 10 Characters." )] 
	public string? PhoneNumber { get; set; }   
}