namespace MyCustomUmbracoProject.Services.Account.Models.Request;

public class RegisterRequestModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string Mobile { get; set; }

    public int CountryId { get; set; }

    public int AId { get; set; }

    public string SignupIp { get; set; }
}