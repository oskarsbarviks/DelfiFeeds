namespace Repositories.DataObject
{
    public class UserProfileDataObject
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int FeedsCount { get; set; }
        public string Category { get; set; }
        public byte[] Picture { get; set; }
    }
}
