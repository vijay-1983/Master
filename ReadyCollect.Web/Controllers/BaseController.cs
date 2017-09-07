using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReadyCollect.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Header()
        {
            return View();
        }
        public IActionResult BreadCrumb()
        {
            return View();
        }
        public IActionResult Sidebar()
        {
            return View();
        }
        public IActionResult QuickSidebar()
        {
            return View();
        }
    }
}