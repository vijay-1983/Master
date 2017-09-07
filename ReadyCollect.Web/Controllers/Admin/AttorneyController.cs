using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadyCollect.Model.Admin;
using ReadyCollect.Infrastructure.Admin;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadyCollect.Web.Controllers.Admin
{  
    public class AttorneyController : Controller
    {
        readonly IRCADAttorneyService attorneyService;
        readonly IRCADCompanyService companyService;

        int USKey;
        int LFKey;

        public AttorneyController(IRCADAttorneyService AttorneyService, IRCADCompanyService CompanyService, IHttpContextAccessor httpAccessor)
        {
            attorneyService = AttorneyService;
            companyService = CompanyService;

            USKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.UserData).Value);
            LFKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value);
        }
        [HttpPost]
        public IActionResult RCADAttorneyAdd(RCADAttorneyModel AttorneyDetails)
        {
            int status = 0;
            attorneyService.InsertAttorney(LFKey, AttorneyDetails, out status);
            return Json(new { success = true });
        }
        [HttpPost]
        public JsonResult RCADAttorney(int page, int filter, string letter)
        {
            int totalCount = 0;
            int count = 5;
            List<RCADAttorneyModel> LstAttorney = attorneyService.GetAttorneys(page, filter, letter, LFKey, out totalCount, count);
            return Json(new { success = true, AttorneyList = LstAttorney, totalRecords = totalCount });
        }
        [HttpPost]
        public IActionResult RCADAttorneyEdit(RCADAttorneyModel AttorneyData)
        {
            attorneyService.UpdateAttorneyProfile(AttorneyData, USKey, AttorneyData.ATpKey);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateStatus(int ATpKey, string status)
        {

            if (status == "true") status = "0";
            else status = "1";

            attorneyService.UpdateAttorneyStatus(ATpKey, status);
            return Json(new { success = true });
        }

        [HttpGet]
        public JsonResult UseCompanyInfo()
        {
            RCADCompanyModel companyInfo = companyService.GetCompanyInfo(LFKey);
            return Json(companyInfo);
        }
    }
}
