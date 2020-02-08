using HostedServices.Interfaces;
using HostedServices.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServices
{
    /// <summary>
    /// IHostedService class for Delif feeds service(must implement IHostedService)
    /// </summary>
    public class DelfiFeedHostedService : IHostedService
    {
        private ILogger<DelfiFeedHostedService> _logger;
        private IDelfiFeedService _delfiService;
        public DelfiFeedHostedService(
            ILogger<DelfiFeedHostedService> logger,
            IDelfiFeedService delfiService)
        {
            _logger = logger;
            _delfiService = delfiService;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _delfiService.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
