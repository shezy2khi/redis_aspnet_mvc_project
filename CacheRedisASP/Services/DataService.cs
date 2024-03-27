//
using System;
using System.Threading.Tasks;
using CacheRedisASP.Models;
using CacheRedisASP.Services;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading;

namespace CacheRedisASP.Services
{


    public class DataService 
    {
        private readonly MyDbContext _context;
        private readonly RedisCacheService _cacheService;
        private readonly ILogger<DataService> _logger;
        private static string dspMsg;

        public DataService(int id)
        {
            Id = id;
        }

        public DataService(MyDbContext context, RedisCacheService cacheService, ILogger<DataService> logger) { 
            _context = context; 
            _cacheService = cacheService; 
            _logger = logger;

              }

        public int Id { get; }

   
    
        public async Task<Transaction> FindDetails(int id)
        {

            var cacheKey = $"data_{id}";
 

           var cachedData = await _cacheService.GetCachedDataAsync(cacheKey);

            // If Data found in cache, fetch and display
            if (cachedData != null)
            {

                var obj = JsonConvert.DeserializeObject<Transaction>(cachedData);

                //return cachedData;
                dspMsg = "Record ID : " + cacheKey + "...Data retrieved from Redis cache.";
                return obj;
            }

            // Data not found in cache, fetch from SQL Server 
            var data = await _context.Transactions.FirstOrDefaultAsync(m => m.TransactionId == id);


            if (data != null)
            {

                string stringData = JsonConvert.SerializeObject(data);
               
                // Cache the fetched data for 5 minutes
                await _cacheService.CacheDataAsync(cacheKey, stringData.ToString(), TimeSpan.FromMinutes(5)); // Cache for 5 minutes

                dspMsg = "Record ID : " + cacheKey + "...Not found in Redis cache and fetched from SQL Server!";
                return data;
            }

           

            return null;

      
        }

        public string DisplayMsg()
        {
            return dspMsg.ToString();
        }

    }

}