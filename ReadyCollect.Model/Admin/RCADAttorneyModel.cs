using System;
using System.Collections.Generic;
using System.Text;

namespace ReadyCollect.Model.Admin
{
    public class RCADAttorneyModel
    {
        public int ATpKey { get; set; }
        public int ATRCKey { get; set; }
        public string ATFirstName { get; set; }
        public string ATLastName { get; set; }
        public string ATFedBar { get; set; }
        public string ATStateBar { get; set; }
        public string ATInitials { get; set; }
        public string ATCompany { get; set; }
        public string ATAddress1 { get; set; }
        public string ATAddress2 { get; set; }
        public string ATContact1 { get; set; }
        public string ATContact2 { get; set; }
        public string ATSignaturePath { get; set; }
        public virtual byte[] ATSignatureFile { get; set; }
        public string ATCity { get; set; }
        public string ATState { get; set; }
        public string ATPostalCode { get; set; }
        public string ATCountry { get; set; }
        public string ATNote { get; set; }
        public string ATPhone1 { get; set; }
        public string ATExt1 { get; set; }
        public string ATFax1 { get; set; }
        public string ATMobile1 { get; set; }
        public string ATEmail1 { get; set; }
        public string ATPhone2 { get; set; }
        public string ATExt2 { get; set; }
        public string ATFax2 { get; set; }
        public string ATMobile2 { get; set; }
        public string ATEmail2 { get; set; }
        public bool ATActive { get; set; }
        public bool ATMain { get; set; }
        public bool ATDelete { get; set; }
        public DateTime ATCreateDate { get; set; }
        public DateTime ATModifyDate { get; set; }
        public DateTime ATDeleteDate { get; set; }
        public DateTime ATReactivateDate { get; set; }
        public int ATCreatedBy { get; set; }
        public int ATModifiedBy { get; set; }
        public int ATDeletedBy { get; set; }
        public int ATReactivatedBy { get; set; }
    }
}
