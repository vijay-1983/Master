using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadyCollect.Model.Admin
{
    public class RCADUserModel
    {
        public int USpKey { get; set; }
        public int USRCKey { get; set; }
        public string USLogin { get; set; }
        [Display(Name = "Password")]
        public string USPassWord { get; set; }
        [Display(Name = "First Name")]
        public string USFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string USLastName { get; set; }
        [Display(Name = "Email")]
        public string USEmail { get; set; }
        [Display(Name = "Phone")]
        public string USPhone { get; set; }
        [Display(Name = "Fax")]
        public string USFax { get; set; }
        public string USType { get; set; }
        [Display(Name = "Avatar")]
        public virtual byte[] USAvatar { get; set; }
        [Display(Name = "First Name")]
        public string USTypeDesc { get; set; }
        [Display(Name = "User Group")]
        public string UGName { get; set; }
        public DateTime USLastActDate { get; set; }
        public bool USActive { get; set; }
        [Display(Name = "Initials")]
        public string USInitials { get; set; }
        public string USGridLayout { get; set; }
        public string USControlAccessOptions { get; set; }
        public bool USCaseBalancedHeaderFixed { get; set; }
        public int USCreatedBy { get; set; }
        public int USMOdifiedBy { get; set; }
        public int USDeletedBy { get; set; }
        public int USReactivatedBy { get; set; }
        public int LFpKey { get; set; }
        public bool USDelete { get; set; }
        public int UGpKey { get; set; }
        public string ImagePath { get; set; }
        public string NewPassword { get; set; }
        public List<RCADUserGroupModel> UserGroups { get; set; }
    }
}