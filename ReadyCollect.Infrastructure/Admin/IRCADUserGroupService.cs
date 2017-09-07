using System;
using System.Collections.Generic;
using System.Text;
using ReadyCollect.Model.Admin;

namespace ReadyCollect.Infrastructure.Admin
{
    public interface IRCADUserGroupService
    {
        List<RCADUserGroupModel> GetGroups(int LFpKey);
    }
}
