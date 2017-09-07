using System;
using System.ComponentModel.DataAnnotations;

namespace ReadyCollect.Model
{
    public class UserLoginModel
    {
        public int USpKey { get; set; }
        public int USRCKey { get; set; }
        public string USFirstName { get; set; }
        public string USLastName { get; set; }
        public string USInitials { get; set; }
        public int LFpKey { get; set; }
        [Required(ErrorMessage = "Please Enter CompanyID")]
        public string CompanyId { get; set; }
        public string LFName { get; set; }
        public int UGpKey { get; set; }
        public string UGName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name="Password")]
        public string USPassword { get; set; }
        public bool Remeberme { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        public string USLogin { get; set; }
    }
}