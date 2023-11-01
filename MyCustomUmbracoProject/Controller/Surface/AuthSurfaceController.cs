using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyCustomUmbracoProject.Models.ViewModels;
using MyCustomUmbracoProject.Security;
using MyCustomUmbracoProject.Services.Account;
using MyCustomUmbracoProject.Services.Account.Models.Request;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace MyCustomUmbracoProject.Controller.Surface;

public class AuthSurfaceController : SurfaceController
{
    private readonly IContentService _contentService;
    private readonly ILogger<AuthSurfaceController> _logger;
    private readonly IAccountService _accountService;
    private readonly CurrentUserContext _currentUserContext;
    private readonly IUmbracoMapper _mapper;

    public AuthSurfaceController(
        ILogger<AuthSurfaceController> logger,
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory umbracoDatabaseFactory,
        ServiceContext serviceContext,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider, 
        IAccountService accountService,
        IContentService contentService, 
        CurrentUserContext currentUserContext,
        IUmbracoMapper mapper)
        : base(umbracoContextAccessor, umbracoDatabaseFactory, serviceContext, appCaches, profilingLogger, publishedUrlProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _accountService = accountService;
        _contentService = contentService;
        _currentUserContext = currentUserContext;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Login(RegisterViewModel model)
    {
        try
        {
            var loginModel = _mapper.Map<LoginRequestModel>(model);
            
            var result = await _accountService.LoginAsync(loginModel);

            if (result.IsSuccessful)
            {
                var contentKey = _contentService.GetRootContent().FirstOrDefault(c => c.Name == "Profile")!.Key;
                TempData["Success"] = "Success";

                return RedirectToUmbracoPage(contentKey);
            }

            TempData["Error"] = "Can't login with credentials";
            return RedirectToCurrentUmbracoUrl();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling SOAP service.");

            TempData["Error"] = "An error occurred. Please try again.";
            return RedirectToCurrentUmbracoUrl();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            var registerModel = _mapper.Map<RegisterRequestModel>(model);
            
            var result = await _accountService.RegisterAsync(registerModel);

            if (result.IsSuccessful)
            {
                var contentKey = _contentService.GetRootContent().FirstOrDefault(c => c.Name == "Profile")!.Key;
                TempData["Success"] = "Success";

                return RedirectToUmbracoPage(contentKey);
            }
            
            TempData["Error"] = "Can't create account with this info.";
            return RedirectToCurrentUmbracoUrl();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling SOAP service.");

            TempData["Error"] = "An error occurred. Please try again.";
            return RedirectToCurrentUmbracoUrl();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _currentUserContext.SignOutAsync();
        return RedirectToCurrentUmbracoUrl();
    }
}