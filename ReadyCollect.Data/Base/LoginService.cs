using System;
using System.Collections.Generic;
using System.Text;
using ReadyCollect.Infrastructure.Base;
using ReadyCollect.Model;
using ReadyCollect.Model.Admin;
using System.Threading;
using System.Threading.Tasks;

namespace ReadyCollect.Data.Base
{
    public class LoginService : ILoginService
    {
        #region "Constructor"
        string connectionString = string.Empty;
        DataHelper<UserLoginModel> dataHelper;

        public LoginService(string conString)
        {
            connectionString = conString;
            dataHelper = new DataHelper<UserLoginModel>(conString);
        }

        #endregion        

        public UserLoginModel ValidateUser(int CompanyID, string USLogin, string USPassword)
        {
            try
            {
                var spParam = new Dapper.DynamicParameters();
                spParam.Add("@ClientKeyint", CompanyID, System.Data.DbType.Int16);
                spParam.Add("@Loginvarchar", USLogin, System.Data.DbType.String);
                spParam.Add("@Passwordvarchar", USPassword, System.Data.DbType.String);

                var data = dataHelper.ExecProcedureWithData("[dbo].[usp_User_ValidateLogin]", spParam);

                if (data != null) return new List<UserLoginModel>(data).Find(d => d.LFpKey == CompanyID);

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

