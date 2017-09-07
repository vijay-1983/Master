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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadyCollect.Web.Controllers.Admin
{
    
    public class UserController : Controller
    {
        readonly IRCADUserService userService;
        readonly IRCADUserGroupService usergroupService;
        private IHostingEnvironment _environment;

        string UserName = string.Empty;
        int USKey;
        int LFKey;

        public UserController(IRCADUserService UserService, IRCADUserGroupService UsergroupService, IHostingEnvironment environment, IHttpContextAccessor httpAccessor)
        {
            userService = UserService;
            usergroupService = UsergroupService;
            _environment = environment;

            USKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.UserData).Value);
            LFKey = Int16.Parse(httpAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value);
        }

        [HttpPost]
        public IActionResult RCADUsers(int page, int filter, string letter)
        {
            int totalCount;
            var users = userService.GetUsers(LFKey, 5, page, letter, filter, out totalCount);

            return Json(new { success = true, UserList = users, totalRecords = totalCount });
        }

        [HttpPost]
        public IActionResult RCADAddNewUser(RCADUserModel usermodel)
        {
            var imagepath = usermodel.ImagePath;
            if (imagepath != null)
                usermodel.USAvatar = System.IO.File.ReadAllBytes(_environment.WebRootPath + imagepath);
            int status = 0;
            userService.InsertUser(USKey, LFKey, usermodel, out status);
            FileInfo file = new FileInfo(_environment.WebRootPath + imagepath);
            if (file.Exists)
            {
                file.Delete();
            }
            return Json(new { success = true });
        }
        #region "Update user profile"
        
        [HttpPost]
        public IActionResult UpdateUserDetail(RCADUserModel usermodel)
        {
            var profileUpdateVal = UserValues.ProfileUpdate.ContactInfo;
            userService.UpdateUserProfile(usermodel, profileUpdateVal, USKey);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult UpdateUserPassword(RCADUserModel usermodel)
        {
            var profileUpdateVal = UserValues.ProfileUpdate.Password;
            userService.UpdateUserProfile(usermodel, profileUpdateVal, USKey);
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult UpdateUserImage(int id)
        {
            var imagefile = HttpContext.Request.Form.Files.First();
            RCADUserModel userModel = new RCADUserModel();
            userModel.USpKey = id;
            if (imagefile.Length > 0)
            {
                using (var fileStream = imagefile.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    userModel.USAvatar = ms.ToArray();
                }
            }
            var profileUpdateVal = UserValues.ProfileUpdate.Image;
            userService.UpdateUserProfile(userModel, profileUpdateVal, USKey);
            return Json(new { success = true });
        }

        #endregion
        #region "Custom Methods"

        [HttpPost]
        public ActionResult UploadImage()
        {
            long size = 0;
            string filename = string.Empty;
            IFormFile image = HttpContext.Request.Form.Files.First();
            if (image.Length > 0)
            {
                filename = ContentDispositionHeaderValue
                        .Parse(image.ContentDisposition)
                        .FileName
                        .Trim('"');
                string filepath = _environment.WebRootPath + "\\assets\\pages\\media\\profile" + $@"\{filename}";
                size += image.Length;
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    image.CopyTo(fs);
                    fs.Flush();
                }
            }
            return Json(filename);
        }

        [HttpPost]
        public IActionResult UpdateUserStatus(int USpKey, string status)
        {
            if (status == "true")
            {
                status = "0";
            }
            else
            {
                status = "1";
            }
            userService.UpdateUserStatusChange(USpKey, status);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult DeleteUser(int USpKey)
        {
            userService.UserDelete(USpKey);
            return Json(new { success = true });
        }
        #endregion
    }
}
