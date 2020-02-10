using System.Collections.Generic;

namespace FrontendLogic.DTO
{
    public class ProfileModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int FeedsCount { get; set; }
        public string Category { get; set; }
        public string Picture { get; set; }
        public List<string> AvailableCategories { get; set; }
    }
}
