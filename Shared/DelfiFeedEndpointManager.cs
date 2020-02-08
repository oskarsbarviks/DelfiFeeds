using Shared.Interfaces;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Class responsible for managing Delfi Feeds Http endpoints
    /// </summary>
    public class DelfiFeedEndpointManager : IDelfiFeedEndpointManager
    {
        /// <summary>
        /// In production environment these values should be taken from DB, so you can add a new endpoints
        /// without code changes and new deployments to production. Same applies for removing endpoints
        /// </summary>
        public List<DelfiFeedEndpoint> GetEndpoints()
        {
            return new List<DelfiFeedEndpoint>
            {
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=aculiecinieks",
                    Name = "Aculiecinieks"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=auto",
                    Name = "Auto"
                }
            };
        }
    }
}
