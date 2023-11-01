namespace MyCustomUmbracoProject.Services.Account.Models.Response;

public class RegisterResponseModel
{
    public int EntityId { get; set; }

    public int AffiliateResultCode { get; set; }

    public string AffiliateResultMessage { get; set; }
    
    public int ResultCode { get; set; }
    
    public string ResultMessage { get; set; }
}