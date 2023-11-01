namespace MyCustomUmbracoProject.Services.Account.Models.Request;

public class GetUserProfileRequestModel
{
    public int EntityID { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
}