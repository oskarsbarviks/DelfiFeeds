using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface IDelfiFeedEndpointManager
    {
        List<DelfiFeedEndpoint> GetEndpoints();
    }
}