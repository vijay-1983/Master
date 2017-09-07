using ReadyCollect.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadyCollect.Infrastructure.Admin
{
    public interface IRCADAttorneyService
    {
        List<RCADAttorneyModel> GetAttorneys(int page, int filter, string letter, int LFpKey, out int totalCount, int count = 5);
        RCADAttorneyModel GetAttorney(string attorneyKey);
        void UpdateAttorneyProfile(RCADAttorneyModel aData, int modifiedBy, int ATpKey);
        void InsertAttorney(int LFpKey, RCADAttorneyModel aData, out int status);
        void UpdateAttorneyStatus(int ATpkey, string status);
    }
}
