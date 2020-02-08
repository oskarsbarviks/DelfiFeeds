using HostedServices.HttpClients.DTO;
using HostedServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HostedServices.HttpClients
{
    /// <summary>
    /// Delfi feed http client
    /// </summary>
    public class DelfiFeedClient : IDelfiFeedClient
    {
        public async Task<List<DelfiFeed>> GetFeedsByUrl(string url)
        {
            // Query delfi endpoint and get response string
            var responseString = await GetHttpResponseString(url);
            // Parse delfi feed xml into DTO
            return GetDelfiFeedsFromXmlString(responseString);
        }

        private async Task<string> GetHttpResponseString(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpRequestException($"Unsuccesfull call to Delfi feed endpoint. Statuscode : {response.StatusCode}");
                }
            }
        }

        private List<DelfiFeed> GetDelfiFeedsFromXmlString(string xmlString)
        {
            var xml = XDocument.Parse(xmlString);
            var feedItems = xml.Descendants("item");
            return feedItems.Select(item => new DelfiFeed
            {
                ID = (string)item.Element("guid"),
                Title = (string)item.Element("title"),
                Link = (string)item.Element("link"),
                Description = (string)item.Element("description"),
                CommentCount = int.Parse((string)item.Element("{http://purl.org/rss/1.0/modules/slash/}comments")),
                PictureUrl = new Uri((string)item.Element("{http://search.yahoo.com/mrss/}content").Attribute("url")),
                PublishDate = DateTime.Parse((string)item.Element("pubDate"))
            }).ToList();
        }
    }
}
