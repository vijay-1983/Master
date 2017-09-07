using System;

namespace ReadyCollect.Constants
{
    public static class SQLConstants
    {
        public const string USP_ValidateUser = "[dbo].[usp_User_ValidateLogin]";
        public const string USP_AddUser = "[dbo].[usp_User_Insert]";
        public const string USP_GetUsers = "[dbo].[usp_User_Select]";
        public const string USP_AddAttorney = "[dbo].[usp_Attorney_Insert]";
        public const string USP_GetAttorney = "[dbo].[usp_Attorney_Select]";
        public const string USP_GetCompanyInfo = "[dbo].[usp_LawFirm_Select]";
        public const string USP_UpdateAttorney = "[dbo].[usp_Attorney_Update]";
        public const string USP_UpdateCompanyInfo = "[dbo].[usp_LawFirm_Update]";
        public const string USP_UpdateUserProfile = "[dbo].[usp_User_Update]";

    }
}
