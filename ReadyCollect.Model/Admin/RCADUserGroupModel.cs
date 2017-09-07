using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReadyCollect.Model.Admin
{
    public class RCADUserGroupModel
    {
        public int UGpKey { get; set; }

        [Display(Name = "Group Name")]
        public string UGName { get; set; }

        [Display(Name = "Group Description")]
        public string UGDescription { get; set; }

        public bool UGActive { get; set; }
        public int LFpKey { get; set; }
    }
}
