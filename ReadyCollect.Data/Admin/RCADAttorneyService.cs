using ReadyCollect.Infrastructure.Admin;
using ReadyCollect.Model.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ReadyCollect.Constants;
using ReadyCollect.Constants.Admin;

namespace ReadyCollect.Data.Admin
{
    public class RCADAttorneyService: IRCADAttorneyService
    {
        #region "Constructor"
        string connectionString = string.Empty;
        DataHelper<RCADAttorneyModel> dataHelper;
        DataHelper<int> dataHelperCount;
        public RCADAttorneyService(string conString)
        {
            connectionString = conString;
            dataHelper = new DataHelper<RCADAttorneyModel>(conString);
            dataHelperCount = new DataHelper<int>(conString);
        }
        #endregion

        public List<RCADAttorneyModel> GetAttorneys(int page, int filter, string letter, int LFpKey, out int totalCount, int count = 5)
        {
            try
            {
                var uParams = new Dapper.DynamicParameters();
                totalCount = 0;

                string query = " SELECT COUNT(*) FROM dbo.[Attorney] ";
                string whereclause = " WHERE LFpKey =" + LFpKey;
                if (letter.ToLower() != "all" && letter != "-1")
                {
                    whereclause = whereclause + "  AND [ATFirstName] Like '" + letter + "%' ";
                }
                if (letter == "-1")
                {
                    whereclause = whereclause + " AND[ATFirstName] Like '" + "[0 - 9] %'";
                }
                if (filter != -1)
                {
                    whereclause = whereclause + " AND [ATActive] =" + filter;
                }
                query = query + whereclause;
                var total = dataHelperCount.SelectData(query);

                if (total.Count > 0) totalCount = total[0];

                uParams.Add("@LFpKey", LFpKey, DbType.Int16);
                uParams.Add("@RecCount", count, DbType.Int16);
                uParams.Add("@StartingChar", letter, DbType.String);
                uParams.Add("@PageNum", page - 1, DbType.Int16);
                uParams.Add("@Filter", filter, DbType.Int16);
                return dataHelper.ExecProcedureWithData(SQLConstants.USP_GetAttorney, uParams);

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public RCADAttorneyModel GetAttorney(string attorneyKey)
        {
            try
            {
                int key = Convert.ToInt32(attorneyKey);
                string query = string.Format("Select * from Attorney where ATpKey={0}", key);
                List<RCADAttorneyModel> RCADAttorneymodel = dataHelper.SelectData(query);
                return RCADAttorneymodel.Find(item => item.ATpKey == key);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void InsertAttorney(int ATpKey, RCADAttorneyModel aData, out int status)
        {
            try
            {
                status = -1;

                //Null check for the data
                if (aData == null) return;

                var aParam = new Dapper.DynamicParameters();

                aParam.Add("@ATFirstName", aData.ATFirstName, DbType.String);
                aParam.Add("@ATLastName", aData.ATLastName, DbType.String);
                aParam.Add("@ATFedBar", aData.ATFedBar, DbType.String);
                aParam.Add("@ATStateBar", aData.ATStateBar, DbType.String);
                aParam.Add("@ATInitials ", aData.ATInitials, DbType.String);
                aParam.Add("@ATCompany", aData.ATCompany, DbType.String);
                aParam.Add("@ATAddress1", aData.ATAddress1, DbType.String);
                aParam.Add("@ATAddress2", aData.ATAddress2, DbType.String);
                aParam.Add("@ATCity", aData.ATCity, DbType.String);
                aParam.Add("@ATState", aData.ATState, DbType.String);
                aParam.Add("@ATPostalCode", aData.ATPostalCode, DbType.String);
                aParam.Add("@ATCountry", aData.ATCountry, DbType.String);
                aParam.Add("@ATNote", aData.ATNote, DbType.String);
                aParam.Add("@ATPhone1", aData.ATPhone1, DbType.String);
                aParam.Add("@ATExt1", aData.ATExt1, DbType.String);
                aParam.Add("@ATFax1", aData.ATFax1, DbType.String);
                aParam.Add("@ATMobile1", aData.ATMobile1, DbType.String);
                aParam.Add("@ATEmail1", aData.ATEmail1, DbType.String);
                aParam.Add("@ATPhone2", aData.ATPhone2, DbType.String);
                aParam.Add("@ATFax2", aData.ATFax2, DbType.String);
                aParam.Add("@ATExt2", aData.ATExt2, DbType.String);
                aParam.Add("@ATMobile2", aData.ATMobile2, DbType.String);
                aParam.Add("@ATEmail2", aData.ATEmail2, DbType.String);
                aParam.Add("@ATCreatedBy", ATpKey, DbType.Int16);
                aParam.Add("@CompanyExists", dbType: DbType.Int16, direction: ParameterDirection.Output);

                dataHelper.ExecProcedure(SQLConstants.USP_AddAttorney, out status, aParam);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateAttorneyProfile(RCADAttorneyModel aData, int modifiedBy, int ATpKey)
        {
            try
            {
                int status = 0;
                if (aData == null) return;

                var aParam = new Dapper.DynamicParameters();

                aParam.Add("@ATFirstName", aData.ATFirstName, DbType.String);
                aParam.Add("@ATLastName", aData.ATLastName, DbType.String);
                aParam.Add("@ATFedBar", aData.ATFedBar, DbType.String);
                aParam.Add("@ATStateBar", aData.ATStateBar, DbType.String);
                aParam.Add("@ATInitials ", aData.ATInitials, DbType.String);
                aParam.Add("@ATCompany", aData.ATCompany, DbType.String);
                aParam.Add("@ATAddress1", aData.ATAddress1, DbType.String);
                aParam.Add("@ATAddress2", aData.ATAddress2, DbType.String);
                aParam.Add("@ATCity", aData.ATCity, DbType.String);
                aParam.Add("@ATState", aData.ATState, DbType.String);
                aParam.Add("@ATPostalCode", aData.ATPostalCode, DbType.String);
                aParam.Add("@ATCountry", aData.ATCountry, DbType.String);
                aParam.Add("@ATNote", aData.ATNote, DbType.String);
                aParam.Add("@ATPhone1", aData.ATPhone1, DbType.String);
                aParam.Add("@ATExt1", aData.ATExt1, DbType.String);
                aParam.Add("@ATFax1", aData.ATFax1, DbType.String);
                aParam.Add("@ATContact1", aData.ATContact1, DbType.String);
                aParam.Add("@ATMobile1", aData.ATMobile1, DbType.String);
                aParam.Add("@ATEmail1", aData.ATEmail1, DbType.String);
                aParam.Add("@ATPhone2", aData.ATPhone2, DbType.String);
                aParam.Add("@ATContact2", aData.ATContact2, DbType.String);
                aParam.Add("@ATFax2", aData.ATFax2, DbType.String);
                aParam.Add("@ATExt2", aData.ATExt2, DbType.String);
                aParam.Add("@ATMobile2", aData.ATMobile2, DbType.String);
                aParam.Add("@ATEmail2", aData.ATEmail2, DbType.String);
                aParam.Add("@ATModifiedBy", modifiedBy, DbType.Int16);
                aParam.Add("@ATpKey", ATpKey, DbType.Int16);

                dataHelper.ExecProcedure(SQLConstants.USP_UpdateAttorney, out status, aParam);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void UpdateAttorneyStatus(int ATpkey, string status)
        {
            try
            {
                string query = "update attorney set ATActive =" + status + " WHERE ATpKey= " + ATpkey;
                int updateval = 0;
                dataHelper.ManageData(query, out updateval);
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}
