using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadyCollect.Model.Admin;
using ReadyCollect.Infrastructure.Admin;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Net.Http.Headers;
using ReadyCollect.Constants.Admin;

namespace ReadyCollect.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly IRCADUserService userService;
        readonly IRCADAttorneyService attorneyService;
        readonly IRCADCompanyService companyService;
        readonly IRCADUserGroupService userGroupService;
        private IHostingEnvironment _environment;
        string UserName = string.Empty;
        int USKey;
        int LFKey;

        public AdminController(IRCADCompanyService CompanyService,IRCADUserService UserService, IRCADAttorneyService AttorneyService, IRCADUserGroupService UserGroupService, IHttpContextAccessor httpAccessor, IHostingEnvironment environment)
        {
            userService = UserService;
            attorneyService = AttorneyService;
            companyService = CompanyService;
            userGroupService = UserGroupService;
            _environment = environment;
            USKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.UserData).Value);
            LFKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value);
        }

        public IActionResult Index()
        {
            return View();
        }

        #region "Users"

        public IActionResult RCADUsers() => View();

        public IActionResult RCADAddUser()
        {
            RCADUserModel usermodel = new RCADUserModel();
            usermodel.UserGroups = userGroupService.GetGroups(LFKey);
            return View(usermodel);
        }


        public IActionResult RCADUpdateUser(string UserKey)
        {
            var userDetail = userService.GetUser(UserKey);
            userDetail.UserGroups = userGroupService.GetGroups(userDetail.UGpKey);
            //ViewBag.GroupName = userDetail.UserGroups.Where(x => x.UGpKey == userDetail.UGpKey).Select(x => x.UGName).First();
            return View(userDetail);
        }
        #endregion

        #region "Attorney"

        public IActionResult RCADAttorney() => View();

        public IActionResult RCADAttorneyAdd() => View();

        public IActionResult RCADAttorneyEdit(string ATpKey)
        {
            return View(attorneyService.GetAttorney(ATpKey));
        }

        #endregion

        #region "Company Info"
        public IActionResult RCADCompany()
        {
            RCADCompanyModel CompanyDetails = companyService.GetCompanyInfo(LFKey);
            return View(CompanyDetails);
        }

        #endregion

    }
}