using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAdvertApi.Contract;

namespace WebAdvertApi.HealthChecks
{
    public class StorageHealth : IHealthCheck
    {
        private readonly IAdvertStorage _storageService;
        public StorageHealth(IAdvertStorage storage)
        {
         _storageService =storage;   
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isStorageOk =await _storageService.CheckHealthAsync();
            return HealthCheckResult.Healthy(isStorageOk?"Healthy":"Unhealthy");
        }
    }
}