using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorLogin.Pages
{
    public class LogOutModel : PageModel
    {
		public async Task<IActionResult> OnGetAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return LocalRedirect(Url.Content("~/"));
		}
	}
}
