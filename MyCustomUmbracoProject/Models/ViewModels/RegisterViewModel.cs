using System.ComponentModel.DataAnnotations;

namespace MyCustomUmbracoProject.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "You must enter a valid email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter your email address")]
    public string Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "You must enter a password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
        ErrorMessage =
            "Password must be at least 8 characters long, have 1 uppercase, 1 lowercase, 1 number and 1 special character.")]
    public string Password { get; set; }

    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "You must confirm the password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Mobile")]
    [Phone(ErrorMessage = "You must enter a valid phone number")]
    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "You must enter your phone number")]
    public string Mobile { get; set; }

    [Display(Name = "CountryID")]
    [Required(ErrorMessage = "You must enter your country id")]
    public int CountryId { get; set; }

    [Display(Name = "aId")]
    [Required(ErrorMessage = "You must enter your aId")]
    public int AId { get; set; }

    [Display(Name = "SignupIP")]
    [Required(ErrorMessage = "You must enter your phone number")]
    [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IP adress must be in correct format.")]
    public string SignupIp { get; set; }
}