namespace MyCustomUmbracoProject.Services.Account.Models.Request;

public class LoginRequestModel
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string IpAddress { get; set; }
}