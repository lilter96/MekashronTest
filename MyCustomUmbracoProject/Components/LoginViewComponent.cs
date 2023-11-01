using Microsoft.AspNetCore.Mvc;
using MyCustomUmbracoProject.Models.ViewModels;

namespace MyCustomUmbracoProject.Components;

[ViewComponent(Name = "Login")]
public class LoginViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(RegisterViewModel model)
    {
        return View(model);
    }
}