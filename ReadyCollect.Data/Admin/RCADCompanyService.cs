using ReadyCollect.Constants;
using ReadyCollect.Infrastructure.Admin;
using ReadyCollect.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadyCollect.Data.Admin
{
    public class RCADCompanyService : IRCADCompanyService
    {
        DataHelper<RCADCompanyModel> dataHelper;

        public RCADCompanyService(string Connnection)
        {
            dataHelper = new DataHelper<RCADCompanyModel>(Connnection);
        }

        public RCADCompanyModel GetCompanyInfo(int LFpKey)
        {
            try
            {
                var uParams = new Dapper.DynamicParameters();

                uParams.Add("@LFpKey", LFpKey, System.Data.DbType.Int16);

                return dataHelper.ExecProcedureWithData(SQLConstants.USP_GetCompanyInfo, uParams).Find(c => c.LFpKey == LFpKey);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCompanyInfo(RCADCompanyModel companyData, int modifiedBy, out int status)
        {
            try
            {
                /*
                    @LFpKey int, @LFpName varchar(250), @LFEmail varchar(250), 
	                @LFAddress1 varchar(150), @LFAddress2 varchar(150), @LFCity varchar(100),
	                @LFState varchar(100), @LFPostalCode varchar(20),
	                @LFCountry varchar(100), @LFBillingContact varchar(150),
	                @LFBillingPhone varchar(20), @LFBillingExt varchar(10),
	                @LFBillingEmail varchar(150), @LFPrimaryContact varchar(250),
	                @LFPrimaryPhone varchar(20), @LFPrimaryExt varchar(10), @LFPrimaryEmail varchar(150),
	                @LFSecondaryContact varchar(250), @LFSecondaryPhone varchar(20),
	                @LFSecondaryExt varchar(10), @LFSecondaryEmail varchar(100),
	                @LFModifiedByUser varchar(250), @LFPrimaryFax varchar(20), @LFPrimaryMobile varchar(20),
	                @LFSecondaryMobile varchar(20), @LFSecondaryFax varchar(20), @LFAlertTitle varchar(500),
	                @LFAlertMessage varchar(1500), @LFAlertStartDate datetime, @LFAlertEndDate datetime,
	                @LFAlertIndefinite bit
                 */
                var uParam = new Dapper.DynamicParameters();

                uParam.Add("@LFpKey", companyData.LFpKey, System.Data.DbType.Int16);
                uParam.Add("@LFpName", companyData.LFName, System.Data.DbType.String);
                uParam.Add("@LFEmail", companyData.LFEmail, System.Data.DbType.String);
                uParam.Add("@LFAddress1", companyData.LFAddres1, System.Data.DbType.String);
                uParam.Add("@LFAddress2", companyData.LFAddress2, System.Data.DbType.String);
                uParam.Add("@LFCity", companyData.LFCity, System.Data.DbType.String);
                uParam.Add("@LFState", companyData.LFState, System.Data.DbType.String);
                uParam.Add("@LFPostalCode", companyData.LFPostalCode, System.Data.DbType.String);
                uParam.Add("@LFCountry", companyData.LFCountry, System.Data.DbType.String);
                uParam.Add("@LFBillingContact", companyData.LFBillingContact, System.Data.DbType.String);
                uParam.Add("@LFBillingEmail", companyData.LFBillingEmail, System.Data.DbType.String);
                uParam.Add("@LFBillingExt", companyData.LFBillingExt, System.Data.DbType.String);
                uParam.Add("@LFBillingPhone", companyData.LFBillingPhone, System.Data.DbType.String);
                uParam.Add("@LFBillingMobile", companyData.LFBillingMobile, System.Data.DbType.String);
                uParam.Add("@LFBillingFax", companyData.LFBillingFax, System.Data.DbType.String);
                uParam.Add("@LFPrimaryContact", companyData.LFPrimaryContact, System.Data.DbType.String);
                uParam.Add("@LFPrimaryPhone", companyData.LFPrimaryPhone, System.Data.DbType.String);
                uParam.Add("@LFPrimaryExt", companyData.LFPrimaryExt, System.Data.DbType.String);
                uParam.Add("@LFPrimaryMobile", companyData.LFPrimaryMobile, System.Data.DbType.String);
                uParam.Add("@LFPrimaryEmail", companyData.LFPrimaryEmail, System.Data.DbType.String);
                uParam.Add("@LFPrimaryFax", companyData.LFPrimaryFax, System.Data.DbType.String);
                uParam.Add("@LFSecondaryContact", companyData.LFSecondaryContact, System.Data.DbType.String);
                uParam.Add("@LFSecondaryPhone", companyData.LFSecondaryPhone, System.Data.DbType.String);
                uParam.Add("@LFSecondaryExt", companyData.LFSecondaryExt, System.Data.DbType.String);
                uParam.Add("@LFSecondaryEmail", companyData.LFSecondaryEmail, System.Data.DbType.String);
                uParam.Add("@LFSecondaryFax", companyData.LFSecondaryFax, System.Data.DbType.String);
                uParam.Add("@LFSecondaryMobile", companyData.LFSecondaryMobile, System.Data.DbType.String);
                uParam.Add("@LFModifiedByUser", modifiedBy, System.Data.DbType.Int16);
                uParam.Add("@LFAlertTitle", companyData.LFAlertTitle, System.Data.DbType.String);
                uParam.Add("@LFAlertMessage", companyData.LFAlertMessage, System.Data.DbType.String);
                uParam.Add("@LFAlertStartDate", companyData.LFAlertStartDate, System.Data.DbType.DateTime);
                uParam.Add("@LFAlertEndDate", companyData.LFAlertEndDate, System.Data.DbType.DateTime);
                uParam.Add("@LFAlertIndefinite", companyData.LFAlertIndefinite, System.Data.DbType.Boolean);

                dataHelper.ExecProcedure(SQLConstants.USP_UpdateCompanyInfo, out status, uParam);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
