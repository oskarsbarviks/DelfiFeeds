namespace Repositories.Constants
{
    /// <summary>
    /// Database procedure name constants
    /// </summary>
    public class ProcedureNameConstants
    {
        public static readonly string InsertOrUpdateFeedByID = "dbo.Sp_InsertOrUpdateFeedByID";
        public static readonly string ChangeFeedCategoryUpdateTime = "dbo.Sp_ChangeFeedCategoryUpdateTime";
        public static readonly string GetUserByID = "dbo.Sp_GetUserByID";
        public static readonly string CreateOrUpdateUser = "dbo.Sp_CreateOrUpdateUser";
        public static readonly string GetFeedSettingsByUserID = "dbo.Sp_GetFeedSettingsByUserID";
        public static readonly string GetFeedsByCategory = "dbo.Sp_GetFeedsByCategory";
        public static readonly string GetUserProfileData = "dbo.Sp_GetUserProfileData";
        public static readonly string UpdateUserAndProfileData = "dbo.Sp_UpdateUserAndProfileData";
    }
}
