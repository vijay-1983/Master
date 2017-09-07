using System;
using System.Collections.Generic;
using System.Text;

namespace ReadyCollect.Model.Admin
{
    public class RCADCompanyModel
    {
        public int LFpKey { get; set; }
        public int LFClientKey { get; set; }
        public string LFName { get; set; }
        public string LFEmail { get; set; }
        public string LFCSS { get; set; }
        public string LFLogo { get; set; }
        public bool LFBackground { get; set; }
        public int LFRCKey { get; set; }
        public string LFIP { get; set; }
        public bool LFActive { get; set; }
        public string LFAddres1 { get; set; }
        public string LFAddress2 { get; set; }
        public string LFCity { get; set; }
        public string LFState { get; set; }
        public string LFPostalCode { get; set; }
        public string LFCountry { get; set; }
        public string LFBillingContact { get; set; }
        public string LFBillingPhone { get; set; }
        public string LFBillingExt { get; set; }
        public string LFBillingEmail { get; set; }
        public string LFBillingMobile { get; set; }
        public string LFBillingFax { get; set; }
        public string LFPrimaryContact { get; set; }
        public string LFPrimaryPhone { get; set; }
        public string LFPrimaryExt { get; set; }
        public string LFPrimaryEmail { get; set; }
        public string LFSecondaryContact { get; set; }
        public string LFSecondaryPhone { get; set; }
        public string LFSecondaryExt { get; set; }
        public string LFSecondaryEmail { get; set; }
        public DateTime LFCreateDate { get; set; }
        public DateTime LFModifyDate { get; set; }
        public DateTime LFDeleteDate { get; set; }
        public DateTime LFReactivateDate { get; set; }
        public int LFCretedByUser { get; set; }
        public int LFModifiedByUser { get; set; }
        public int LFDeletedByUser { get; set; }
        public int LFReactivatedByUser { get; set; }
        public string LFPrimaryFax { get; set; }
        public string LFPrimaryMobile { get; set; }
        public string LFSecondaryFax { get; set; }
        public string LFSecondaryMobile { get; set; }
        public string LFAlertTitle { get; set; }
        public string LFAlertMessage { get; set; }
        public DateTime? LFAlertStartDate { get; set; }
        public DateTime? LFAlertEndDate { get; set; }
        public bool LFAlertIndefinite { get; set; }
    }
}
