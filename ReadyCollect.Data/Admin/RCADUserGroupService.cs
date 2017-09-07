using ReadyCollect.Infrastructure.Admin;
using ReadyCollect.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadyCollect.Data.Admin
{
    public class RCADUserGroupService : IRCADUserGroupService
    {
        DataHelper<RCADUserGroupModel> dataHelper;

        public RCADUserGroupService(string conString)
        {
            dataHelper = new DataHelper<RCADUserGroupModel>(conString);
        }

        public List<RCADUserGroupModel> GetGroups(int LFpKey)
        {
            try
            {
                return dataHelper.SelectData("Select * From dbo.UserGroup Where LFpKey = " + LFpKey);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
