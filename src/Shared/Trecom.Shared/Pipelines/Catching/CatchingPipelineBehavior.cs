using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Trecom.Shared.Pipelines.Catching
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICachableQueryRequest
    {
        private readonly IDistributedCache cache;
        private readonly ILogger<CachingPipelineBehavior<TRequest, TResponse>> logger;
        private readonly CacheSettings cacheSettings;

        public CachingPipelineBehavior(IDistributedCache cache, ILogger<CachingPipelineBehavior<TRequest, TResponse>> logger, IOptions<CacheSettings> cacheSettings)
        {
            this.cache = cache;
            this.logger = logger;
            this.cacheSettings = cacheSettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            if (request.BypassCache) return await next();

            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromMinutes(cacheSettings.SlidingExpiration);
                DistributedCacheEntryOptions opt = new() { SlidingExpiration = slidingExpiration };
                byte[]? serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await cache.SetAsync((string)request.CacheKey, serializedData, opt, cancellationToken);
                return response;
            }

            var cachedResponse = await cache.GetAsync((string)request.CacheKey, cancellationToken);

            if (cachedResponse != null)
            {
                
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
                logger.LogInformation($"Pulled data from Cache => '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                logger.LogInformation($"Added to Cache => '{request.CacheKey}'.");
            }
            return response;
        }
    }
}
