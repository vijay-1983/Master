using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadyCollect.Infrastructure.Admin;
using Microsoft.AspNetCore.Http;
using ReadyCollect.Model.Admin;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadyCollect.Web.Controllers.Admin
{
    public class CompanyController : Controller
    {
        readonly IRCADCompanyService companyService;
        int USKey;

        public CompanyController(IRCADCompanyService CompanyService, IHttpContextAccessor httpAccessor)
        {
            companyService = CompanyService;

            USKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.UserData).Value);
          
        }

        [HttpPost]
        public IActionResult RCADCompanyUpdate(RCADCompanyModel CompanyDetails)
        {
            int status = 0;
            companyService.UpdateCompanyInfo(CompanyDetails, USKey, out status);
            return Json(new { success = true });
        }
    }
}
