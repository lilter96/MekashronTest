using Microsoft.AspNetCore.Mvc;

namespace MyCustomUmbracoProject.Components;

[ViewComponent(Name = "Home")]
public class HomeViewController : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}