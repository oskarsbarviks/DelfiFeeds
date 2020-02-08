using FrontendLogic.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontendLogic.FacebookClient
{
    public class FacebookClient : IFacebookClient
    {
        private readonly string _userDataEndpoint = "https://graph.facebook.com/me?fields=name,email&access_token={accessToken}";
        private readonly string _userProfilePictureEndpoint = "https://graph.facebook.com/{userID}/picture?width=100&height=100";
        public async Task<FacebookUserDto> GetUserDataFromAccessToken(string accessToken)
        {
            var url = _userDataEndpoint.Replace("{accessToken}", accessToken);
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"User data fetching from Facebook failed. Statuscode: {response.StatusCode}");
                }

                var contentString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<FacebookUserDto>(contentString);
            }
        }

        public async Task<byte[]> GetPictureByteArrayByUserId(string userID) 
        {
            var url = _userProfilePictureEndpoint.Replace("{userID}", userID);
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"User data fetching from Facebook failed. Statuscode: {response.StatusCode}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
        }
    }
}
