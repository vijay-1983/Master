using System;
using System.Collections.Generic;
using System.Text;
using ReadyCollect.Model.Admin;
using ReadyCollect.Infrastructure.Admin;
using System.Data;
using ReadyCollect.Constants;
using ReadyCollect.Constants.Admin;

namespace ReadyCollect.Data.Admin
{
    public class RCADUserService : IRCADUserService
    {
        #region "Constructor"
        string connectionString = string.Empty;
        DataHelper<RCADUserModel> dataHelper;
        DataHelper<int> dataHelperCount;

        public RCADUserService(string conString)
        {
            connectionString = conString;
            dataHelper = new DataHelper<RCADUserModel>(conString);
            dataHelperCount = new DataHelper<int>(conString);
        }
        #endregion

        public RCADUserModel GetUser(string userKey)
        {
            string query = "SELECT * FROM dbo.[users] WHERE USpKey ='" + userKey + "'";
            return dataHelper.SelectData(query).Find(u => u.USpKey == int.Parse(userKey));
        }

        public List<RCADUserModel> GetUsers(int companyKey, int count, int page, string letter, int filter, out int totalcount)
        {
            totalcount = 0;
            string query = " SELECT COUNT(*) FROM dbo.[Users] ";
            string whereclause = string.Format(" WHERE LFpKey = {0} and USDelete = 0 ", companyKey);

            if (letter.ToLower() != "all" && letter != "-1")
            {
                whereclause = whereclause + "  AND [USFirstName] Like '" + letter + "%' ";
            }
            if (letter == "#")
            {
                whereclause = whereclause + " AND[USLastName] Like '" + "[0 - 9] %'";
            }
            if (filter != -1)
            {
                whereclause = whereclause + " AND [USActive] =" + filter;
            }
            query = query + whereclause;
            var total = dataHelperCount.SelectData(query);

            if (total.Count > 0) totalcount = total[0];

            var uParam = new Dapper.DynamicParameters();

            uParam.Add("@LFpKey", companyKey , DbType.Int16);
            uParam.Add("@RecCount", count, DbType.Int16);
            uParam.Add("@PageNo", page - 1, DbType.Int16);
            uParam.Add("@StartingChar", letter, DbType.String);
            uParam.Add("@Filter", filter, DbType.Int16);

            var data = dataHelper.ExecProcedureWithData(SQLConstants.USP_GetUsers, uParam);

            return data;
        }

        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        public void InsertUser(int CreateUser, int LFpKey, RCADUserModel uData, out int status)
        {
            try
            {
                status = -1;

                //Null check for the data
                if (uData == null) return;

                var uParam = new Dapper.DynamicParameters();

                uParam.Add("@CreatedUser", CreateUser, DbType.Int16);
                uParam.Add("@USFirstName", uData.USFirstName, DbType.String);
                uParam.Add("@USLastName", uData.USLastName, DbType.String);
                uParam.Add("@USLogin", uData.USLogin, DbType.String);
                uParam.Add("@USInitials", uData.USInitials, DbType.String);
                uParam.Add("@USEmail", uData.USEmail, DbType.String);
                uParam.Add("@USPhone", uData.USPhone, DbType.String);
                uParam.Add("@USFax", uData.USFax, DbType.String);
                uParam.Add("@USPassWord", uData.USPassWord, DbType.String);
                uParam.Add("@USAvatar", uData.USAvatar, DbType.Binary);
                uParam.Add("@UGpKey", uData.UGpKey, DbType.Int16);
                uParam.Add("@LFpKey", LFpKey, DbType.Int16);
                uParam.Add("@EmailExists", dbType: DbType.Int16, direction: ParameterDirection.Output);

                dataHelper.ExecProcedure(SQLConstants.USP_AddUser, out status, uParam);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateUserProfile(RCADUserModel data, UserValues.ProfileUpdate updateProfile, int modifiedBy)
        {
            try
            {
                if (data == null) return;
                int status = 0;

                var uParams = new Dapper.DynamicParameters();
                uParams.Add("@USpKey", data.USpKey, DbType.Int16);
                uParams.Add("@USFirstName", data.USFirstName, DbType.String);
                uParams.Add("@USLastName", data.USLastName, DbType.String);
                uParams.Add("@USLoginName", data.USLogin, DbType.String);
                uParams.Add("@USInitials", data.USInitials, DbType.String);
                uParams.Add("@USEmail", data.USEmail, DbType.String);
                uParams.Add("@USPhone", data.USPhone, DbType.String);
                uParams.Add("@USFax", data.USFax, DbType.String);
                uParams.Add("@USPassword", data.NewPassword, DbType.String);
                uParams.Add("@USAvatar", data.USAvatar, DbType.Binary);
                uParams.Add("@UpdateProfile", Convert.ToInt16(updateProfile), DbType.Int16);
                uParams.Add("@ModifiedUser", modifiedBy, DbType.String);

                dataHelper.ExecProcedure(SQLConstants.USP_UpdateUserProfile, out status, uParams);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateUserStatusChange(int USpKey, string status)
        {
            string query = "update [ReadyCollect].[dbo].[Users] set USActive =" + status + " WHERE USpKey= " + USpKey;
            int updateval = 0;
            dataHelper.ManageData(query, out updateval);
        }
        public void UserDelete(int USpKey)
        {
            string query = "update [ReadyCollect].[dbo].[Users] set USDelete = 1 WHERE USpKey= " + USpKey;
            int updateval = 0;
            dataHelper.ManageData(query, out updateval);
        }
    }
}
