using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BlazorLogin.Services;
using Microsoft.AspNetCore.Authorization;

namespace BlazorLogin.Pages
{
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		private readonly UserService _userService;

		public LoginModel(UserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> OnGetAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return LocalRedirect("/");

			string returnUrl = Url.Content("~/");
			try
			{
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			}
			catch { }

			var isValidUser = await _userService.CheckDatabaseIfPasswordMatches(username, password);

			if(!isValidUser)
			{
				return LocalRedirect(returnUrl);
			}
			
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.Role, "Admin"),
			};
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = true,
				RedirectUri = this.Request.Host.Value
			};
			try
			{
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
			}
			catch (Exception ex)
			{
				string error = ex.Message;

			}
			return LocalRedirect(returnUrl);
		}
	}

}
