using HostedServices.Extensions;
using HostedServices.Interfaces;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Shared.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServices.Services
{
    /// <summary>
    /// Background service class responsible for delfi feed gathering and inserting in database
    /// </summary>
    public class DelfiFeedService : IDelfiFeedService
    {
        private ILogger<DelfiFeedService> _logger;
        private IDelfiFeedClient _delfiClient;
        private IDelfiFeedEndpointManager _endpointManager;
        private IFeedRepository _feedRepository;
        private IFeedsUpdateTimeRepository _feedsUpdateTimeRepository;
        public DelfiFeedService
            (
            ILogger<DelfiFeedService> logger,
            IDelfiFeedClient delfiCLient,
            IDelfiFeedEndpointManager endpointManager,
            IFeedRepository feedRepository,
            IFeedsUpdateTimeRepository feedsUpdateTimeRepository
            )
        {
            _logger = logger;
            _delfiClient = delfiCLient;
            _endpointManager = endpointManager;
            _feedRepository = feedRepository;
            _feedsUpdateTimeRepository = feedsUpdateTimeRepository;
        }
        public void Start()
        {
            // start service task
            Task.Run(async () => await RunServiceTask());
            return;
        }

        private async Task RunServiceTask()
        {
            do
            {
                // do it in try catch, so that exception does not stop never ending service
                try
                {
                    await StartServiceRoutine();
                    // change it bigger timer if needed
                    Thread.Sleep(600000);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception {nameof(DelfiFeedService)}. Message: {ex.Message}");
                    _logger.LogError($"Stacktrace: {ex.StackTrace}");
                }

            } while (true);
        }

        private async Task StartServiceRoutine()
        {
            _logger.LogInformation("Starting Delfi feed service scheduled routine");
            // get all available endpoints
            foreach (var endpoint in _endpointManager.GetEndpoints())
            {
                // get latest feeds from category
                var feeds = await _delfiClient.GetFeedsByUrl(endpoint.EndpointUrl);
                var databaseFeeds = feeds.ToDatabaseObject(endpoint.Name);
                var hasNewRecordsAddedToCategory = await _feedRepository.UpdateOrInsertFeeds(databaseFeeds);
                // if new records has been added for category, modify category update time
                if (hasNewRecordsAddedToCategory)
                {
                    await _feedsUpdateTimeRepository.ChangeFeedsCategoryUpdateTime(endpoint.Name, DateTime.UtcNow);
                }
            }
        }
    }
}
