using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ReadyCollect.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult RCLGDash()
        {
            ViewBag.Title = HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.System).Value; 
            return View();
        }

        public IActionResult RCLGLogoff()
        {
            HttpContext.Authentication.SignOutAsync("MyCookeiAuthentication");
            return RedirectToAction("Index", "RCLGLogin");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
