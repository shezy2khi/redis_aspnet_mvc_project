
using System;
using System.Threading.Tasks;
using CacheRedisASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

 

namespace CacheRedisASP.Services
{

    public class RedisCacheService 
    //public class RedisCacheService  
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> _logger;
        private readonly IConfiguration _configuration;
        //public const string SessionKeyMsg = "_Msg";

        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger, IConfiguration configuration) 
        { 
            _cache = cache; 
            _logger = logger; 
            _configuration = configuration; 
        }

        public async Task<string> GetCachedDataAsync(string cacheKey)
        {

            

            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);

                if (cachedData != null)
                {
                    //    if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyMsg)))
                    //    {
                    //        HttpContext.Session.SetString(SessionKeyMsg, "This is a message");

                    //    }

                    _logger.LogInformation("Data retrieved from Redis cache.");
                                         
                    return cachedData;
                }
                else
                {
                    _logger.LogInformation("Data not found in Redis cache.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from Redis cache.");
                return null;
            }
        }

        public async Task CacheDataAsync(string cacheKey, string data, TimeSpan expiration)
        {
            try
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                };

                await _cache.SetStringAsync(cacheKey, data, options);
                _logger.LogInformation("Data cached successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error caching data.");
            }
        }
    }

}