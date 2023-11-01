using System.ServiceModel;
using MyCustomUmbracoProject.Helpers;
using MyCustomUmbracoProject.Models.ViewModels;
using MyCustomUmbracoProject.Security;
using MyCustomUmbracoProject.Services.Account.Models.Request;
using MyCustomUmbracoProject.Services.Account.Models.Response;
using Newtonsoft.Json;
using ServiceReference;

namespace MyCustomUmbracoProject.Services.Account;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly CurrentUserContext _currentUserContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(
        ILogger<AccountService> logger,
        CurrentUserContext currentUserContext, 
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _currentUserContext = currentUserContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<LoginResponseModel>> LoginAsync(LoginRequestModel model)
    {
        var client = new ICUTechClient();

        try
        {
            model.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            
            var result = await client.LoginAsync(model.Username, model.Password, model.IpAddress);
            var loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(result.@return)!;

            if (loginResponseModel.ResultCode != 0)
                return CreateErrorResponse<LoginResponseModel>(loginResponseModel.ResultMessage);

            await _currentUserContext.SignInAsync(model.Username, loginResponseModel.EntityId.ToString(),
                model.Password);

            return CreateSuccessResponse(loginResponseModel);
        }
        catch (FaultException ex)
        {
            var errorMessage = "Account Service. WCF throw an error";
            _logger.LogError(ex, errorMessage);
            return CreateErrorResponse<LoginResponseModel>(errorMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = "Account Service. Something went wrong.";
            _logger.LogError(ex, errorMessage);
            return CreateErrorResponse<LoginResponseModel>(errorMessage);
        }
        finally
        {
            client.SafeClose();
        }
    }

    public async Task<Response<RegisterResponseModel>> RegisterAsync(RegisterRequestModel model)
    {
        var client = new ICUTechClient();

        try
        {
            model.SignupIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            
            var result = await client.RegisterNewCustomerAsync(
                model.Email,
                model.Password,
                model.FirstName,
                model.LastName,
                model.Mobile,
                model.CountryId,
                model.AId,
                model.SignupIp);

            var loginResponseModel = JsonConvert.DeserializeObject<RegisterResponseModel>(result.@return)!;

            if (loginResponseModel.ResultCode != 1 || loginResponseModel.EntityId <= 0)
            {
                var errorMessage = "Register. Can't create new customer using RegisterNewCustomer. " +
                                   loginResponseModel.ResultMessage;
                _logger.LogError(errorMessage);
                return CreateErrorResponse<RegisterResponseModel>(errorMessage);
            }

            var fullName = $"{model.FirstName} {model.LastName}";

            await _currentUserContext.SignInAsync(model.Email, loginResponseModel.EntityId.ToString(), model.Password,
                fullName);

            return CreateSuccessResponse(loginResponseModel);
        }
        catch (FaultException ex)
        {
            const string errorMessage = "Account Service. WCF throw an error during registration.";
            _logger.LogError(ex, errorMessage);
            return CreateErrorResponse<RegisterResponseModel>(errorMessage);
        }
        catch (Exception ex)
        {
            const string errorMessage = "Account Service. Registration failed.";
            _logger.LogError(ex, errorMessage);
            return CreateErrorResponse<RegisterResponseModel>(errorMessage);
        }
        finally
        {
            client.SafeClose();
        }
    }

    public async Task<Response<GetUserProfileResponseModel>> GetCurrentUserProfile()
    {
        var client = new ICUTechClient();

        try
        {
            var currentUser = _currentUserContext.GetCurrentUser();

            if (currentUser == null)
            {
                return CreateErrorResponse<GetUserProfileResponseModel>("User is not authenticated");
            }

            var result =
                await client.GetCustomerInfoAsync(currentUser.EntityId, currentUser.Email, currentUser.Password);
            var loginResponseModel = JsonConvert.DeserializeObject<GetUserProfileResponseModel>(result.@return)!;

            return CreateSuccessResponse(loginResponseModel);
        }
        catch (FaultException ex)
        {
            var errorMessage = "Account Service. WCF throw an error";
            _logger.LogError(ex, errorMessage);
            return CreateErrorResponse<GetUserProfileResponseModel>(errorMessage);
        }
        catch (Exception ex)
        {
            var errorMessage = "Account Service. Something went wrong.";
            _logger.LogError(ex, errorMessage);
            return CreateErrorResponse<GetUserProfileResponseModel>(errorMessage);
        }
        finally
        {
            client.SafeClose();
        }
    }

    public async Task Logout()
    {
        try
        {
            await _currentUserContext.SignOutAsync();
        }
        catch (Exception ex)
        {
            var errorMessage = "Account Service. Logout failed.";
            _logger.LogError(ex, errorMessage);
        }
    }

    private static Response<T> CreateSuccessResponse<T>(T data)
    {
        return new Response<T>
        {
            IsSuccessful = true,
            Data = data
        };
    }

    private static Response<T> CreateErrorResponse<T>(string message)
    {
        return new Response<T>
        {
            IsSuccessful = false,
            Message = message
        };
    }
}