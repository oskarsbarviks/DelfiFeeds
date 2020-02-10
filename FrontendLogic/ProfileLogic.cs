using FrontendLogic.DTO;
using FrontendLogic.Interfaces;
using Repositories.Interfaces;
using Shared;
using Shared.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FrontendLogic
{
    public class ProfileLogic : IProfileLogic
    {
        private IUserProfileRepository _userProfileRepository;
        private IDelfiFeedEndpointManager _endpointManager;
        public ProfileLogic(IUserProfileRepository userProfileRepository, IDelfiFeedEndpointManager endpointManager)
        {
            _userProfileRepository = userProfileRepository;
            _endpointManager = endpointManager;
        }
        public async Task<ProfileModel> GetProfileDataByID(string userID)
        {
            var profileDataObject = await _userProfileRepository.GetUserProfileDataByUserID(userID, ProfileSettingsConstants.DefaultCategory, ProfileSettingsConstants.DefaultFeedCount);

            return new ProfileModel 
            {
                FullName = profileDataObject.FullName,
                Category = profileDataObject.Category,
                Email = profileDataObject.Email,
                FeedsCount = profileDataObject.FeedsCount,
                Picture = Convert.ToBase64String(profileDataObject.Picture, 0, profileDataObject.Picture.Length),
                AvailableCategories = _endpointManager.GetEndpoints()
                                                      .Select(endpoint => endpoint.Name)
                                                      .ToList()            
            };
        }
    }
}
