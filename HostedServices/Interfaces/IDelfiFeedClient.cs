using HostedServices.HttpClients.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HostedServices.Interfaces
{
    public interface IDelfiFeedClient
    {
        Task<List<DelfiFeed>> GetFeedsByUrl(string url);
    }
}