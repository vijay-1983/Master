using System;
using System.Collections.Generic;
using System.Text;
using ReadyCollect.Model.Admin;


namespace ReadyCollect.Infrastructure.Admin
{
    public interface IRCADUserService
    {
        List<RCADUserModel> GetUsers(int companyKey, int count, int page, string letter, int filter, out int totalcount);
        RCADUserModel GetUser(string userKey);
        void UpdateUserProfile(RCADUserModel data, Constants.Admin.UserValues.ProfileUpdate updateProfile, int modifiedBy);
        void InsertUser(int CreateUser, int LFpKey, RCADUserModel uData, out int status);
        void UpdateUserStatusChange(int USpKey, string status);
        void UserDelete(int USpKey);
    }
}
