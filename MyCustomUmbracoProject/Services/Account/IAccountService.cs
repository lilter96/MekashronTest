using MyCustomUmbracoProject.Models.ViewModels;
using MyCustomUmbracoProject.Services.Account.Models.Request;
using MyCustomUmbracoProject.Services.Account.Models.Response;

namespace MyCustomUmbracoProject.Services.Account;

public interface IAccountService
{
    public Task<Response<RegisterResponseModel>> RegisterAsync(RegisterRequestModel model);

    public Task<Response<LoginResponseModel>> LoginAsync(LoginRequestModel model);
    
    public Task<Response<GetUserProfileResponseModel>> GetCurrentUserProfile();
}