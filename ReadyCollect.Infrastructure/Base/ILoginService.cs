using ReadyCollect.Model;
using ReadyCollect.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadyCollect.Infrastructure.Base
{
    public interface ILoginService
    {
        UserLoginModel ValidateUser(int CompanyID, string USLogin, string USPassword);
    }
}
