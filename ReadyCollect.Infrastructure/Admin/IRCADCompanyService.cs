using System;
using System.Collections.Generic;
using System.Text;
using ReadyCollect.Model.Admin;

namespace ReadyCollect.Infrastructure.Admin
{
    public interface IRCADCompanyService
    {
        RCADCompanyModel GetCompanyInfo(int LFpKey);
        void UpdateCompanyInfo(RCADCompanyModel companyData, int modifiedBy, out int status);
    }
}
