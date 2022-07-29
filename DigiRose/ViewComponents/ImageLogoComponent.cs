using DigiRose.ModuleServices.CoreAuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace DigiRose.ViewComponents;

public class ImageLogoComponent:ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult)View("default"));
    }
}