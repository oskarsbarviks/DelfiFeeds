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
    }
}
