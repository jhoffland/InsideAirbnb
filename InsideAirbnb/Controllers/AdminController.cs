using InsideAirbnb.Repositories;
using InsideAirbnb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace InsideAirbnb.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IListingRepository _listingRepo;
        private readonly IDistributedCache _cache;

        private JsonSerializerOptions _serializerOptions;
        private DistributedCacheEntryOptions _cacheOptions;

        public AdminController(IListingRepository listingRepo, IDistributedCache cache)
        {
            _listingRepo = listingRepo; 
            _cache = cache;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };
        }

        public async Task<IActionResult> Index()
        {
            string cacheKeyNeighStats = $"neighbourhoodReviewStats-cache";
            var cacheItemNeighStats = await _cache.GetStringAsync(cacheKeyNeighStats);

            string resultNeighStats;
            if (cacheItemNeighStats != null)
            {
                resultNeighStats = cacheItemNeighStats;
            } else
            {
                resultNeighStats = JsonSerializer.Serialize(await _listingRepo.NeighbourhoodReviewStats(), _serializerOptions);
                _cache.SetStringAsync(cacheKeyNeighStats, resultNeighStats, _cacheOptions);
            }

            string cacheKeyPropStats = $"propertyGuestsStats-cache";
            var cacheItemPropStats = await _cache.GetStringAsync(cacheKeyPropStats);

            string resultPropStats;
            if (cacheItemNeighStats != null)
            {
                resultPropStats = cacheItemPropStats;
            }
            else
            {
                resultPropStats = JsonSerializer.Serialize(await _listingRepo.PropertyGuestsStats(), _serializerOptions);
                _cache.SetStringAsync(cacheKeyPropStats, resultPropStats, _cacheOptions);
            }

            var adminChartsData = new AdminChartsDataViewModel
            {
                NeighbourhoodReviewStats = resultNeighStats,
                PropertyGuestsStats = resultPropStats
            };

            return View(adminChartsData);
        }
    }
}
