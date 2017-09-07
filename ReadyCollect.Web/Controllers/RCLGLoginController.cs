using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadyCollect.Infrastructure.Base;
using ReadyCollect.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ReadyCollect.Web.Controllers
{
    public class RCLGLoginController : Controller
    {
        readonly ILoginService loginService;

        public RCLGLoginController(ILoginService logService)
        {
            loginService = logService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index(UserLoginModel userInfo, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {                
                if (userInfo != null)
                {
                    bool isLoggedIn = false;
                    int lfKey = 0;

                    if (string.IsNullOrEmpty(userInfo.CompanyId) || userInfo.CompanyId.Length <= 3)
                    {
                        ViewBag.scriptCall = "ForgotForm();";
                        return View();
                    }

                    if (userInfo.CompanyId.Substring(0, 3).ToLower() != "ar0")
                    {
                        ViewBag.scriptCall = "ForgotForm();";
                        return View();
                    }

                    if (!Int32.TryParse(userInfo.CompanyId.Substring(3), out lfKey))
                    {
                        ViewBag.scriptCall = "ForgotForm();";
                        return View();
                    }

                    userInfo.LFpKey = lfKey;
                    var user = loginService.ValidateUser(userInfo.LFpKey, userInfo.USLogin, userInfo.USPassword);

                    isLoggedIn = (user != null && user.USpKey > 0 ? true : false);

                    if (isLoggedIn)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, string.Concat(user.USFirstName," ",user.USLastName, " ",user.USInitials)),
                            new Claim(ClaimTypes.Sid, user.LFpKey.ToString()),
                            new Claim(ClaimTypes.System, user.LFName),
                            new Claim(ClaimTypes.UserData, user.USpKey.ToString()),
                            new Claim(ClaimTypes.Role, user.UGName)
                        };
                        var props = new AuthenticationProperties();
                        if (userInfo.Remeberme)
                            props.ExpiresUtc = DateTime.UtcNow.AddDays(2);
                        else
                            props.ExpiresUtc = DateTime.UtcNow.AddMinutes(10);

                        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                        await HttpContext.Authentication.SignInAsync("MyCookeiAuthentication", principal, props);

                        if (!string.IsNullOrEmpty(ReturnUrl)) return Redirect(ReturnUrl);

                        return RedirectToAction("RCLGDash", "Home");
                    }
                }
            }

            ViewBag.scriptCall = "ForgotForm();";
            return View();
        }
    }
}
