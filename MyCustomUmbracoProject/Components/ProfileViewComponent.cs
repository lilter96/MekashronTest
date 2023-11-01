using Microsoft.AspNetCore.Mvc;
using MyCustomUmbracoProject.Models.ViewModels;
using MyCustomUmbracoProject.Services.Account;
using Umbraco.Cms.Core.Mapping;

namespace MyCustomUmbracoProject.Components;

[ViewComponent(Name = "Profile")]
public class ProfileViewComponent : ViewComponent
{
    private readonly IAccountService _accountService;
    private readonly IUmbracoMapper _mapper;

    public ProfileViewComponent(IAccountService accountService, IUmbracoMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var currentUserProfile = await _accountService.GetCurrentUserProfile();
        var result = _mapper.Map<ProfileViewModel>(currentUserProfile.Data);
        
        return View(result);
    }
}