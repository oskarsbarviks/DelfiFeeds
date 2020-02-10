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
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=bizness",
                    Name = "Bizness"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=calis",
                    Name = "Calis"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=delfi",
                    Name = "Delfi"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=izklaide",
                    Name = "Izklaide"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=kultura",
                    Name = "Kultura"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=laikazinas",
                    Name = "Laikazinas"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=majadarzs",
                    Name = "Majadarzs"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=mansdraugs",
                    Name = "Mansdraugs"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=orakuls",
                    Name = "Orakuls"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=receptes",
                    Name = "Receptes"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=skats",
                    Name = "Skats"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=sports",
                    Name = "Sports"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=tasty",
                    Name = "Tasty"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=tavamaja",
                    Name = "Tavamaja"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=turismagids",
                    Name = "Turismagids"
                },
                new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=tv",
                    Name = "Tv"
                },
                 new DelfiFeedEndpoint
                {
                    EndpointUrl = "https://www.delfi.lv/rss/?channel=vina",
                    Name = "vina"
                }
            };
        }
    }
}
