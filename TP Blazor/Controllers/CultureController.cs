using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TP_Blazor.Controllers;

[Route("[controller]/[action]")]
public class CultureController:Controller
{
    public IActionResult SetCulture(string culture, string returnUrl)
    {
        if(culture != null)
        {
            this.HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
        }
        return this.LocalRedirect(returnUrl);
    }
}