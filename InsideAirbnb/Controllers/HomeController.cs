using InsideAirbnb.Models;
using InsideAirbnb.Repositories;
using InsideAirbnb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBNBContext _context;
        private readonly IListingSummaryRepository _listingSummaryRepo;
        private readonly IDistributedCache _cache;

        private JsonSerializerOptions _serializerOptions;
        private DistributedCacheEntryOptions _cacheOptions;


        public HomeController(AirBNBContext context, IListingSummaryRepository listingSummaryRepo, IDistributedCache cache)
        {
            _context = context;
            _listingSummaryRepo = listingSummaryRepo;
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
            List<NeighbourhoodViewModel> neighbourhoods = await _context.Neighbourhoods.Select(n => new NeighbourhoodViewModel
            {
                Neighbourhood = n.Neighbourhood1
            })
                .OrderBy(neighbourhoods => neighbourhoods.Neighbourhood)
                .AsNoTracking()
                .ToListAsync();

            return View(neighbourhoods);
        }

        public async Task<IActionResult> Listings([FromQuery(Name = "price-min")] string priceMin, [FromQuery(Name = "price-max")] string priceMax, [FromQuery(Name = "neighbourhood")] string neighbourhood, [FromQuery(Name = "rating-min")] string ratingMin, [FromQuery(Name = "rating-max")] string ratingMax)
        {
            Filter filter = new Filter(priceMin, priceMax, neighbourhood, ratingMin, ratingMax);

            string cacheKey = $"lisitings_{filter.CacheKey}-cache";
            var cacheItem = await _cache.GetStringAsync(cacheKey);

            if(cacheItem != null)
            {
                return Ok(cacheItem);
            }

            string response = JsonSerializer.Serialize(await _listingSummaryRepo.Filter(filter), _serializerOptions);

            _cache.SetStringAsync(cacheKey, response, _cacheOptions);

            return Ok(response);
        }

        public async Task<IActionResult> RoomTypeStats([FromQuery(Name = "price-min")] string priceMin, [FromQuery(Name = "price-max")] string priceMax, [FromQuery(Name = "neighbourhood")] string neighbourhood, [FromQuery(Name = "rating-min")] string ratingMin, [FromQuery(Name = "rating-max")] string ratingMax)
        {
            Filter filter = new Filter(priceMin, priceMax, neighbourhood, ratingMin, ratingMax);

            string cacheKey = $"roomTypeStats_{filter.CacheKey}-cache";
            var cacheItem = await _cache.GetStringAsync(cacheKey);

            if (cacheItem != null)
            {
                return Ok(cacheItem);
            }

            string response = JsonSerializer.Serialize(await _listingSummaryRepo.RoomTypeStats(filter), _serializerOptions);

            _cache.SetStringAsync(cacheKey, response, _cacheOptions);

            return Ok(response);
        }

        public async Task<IActionResult> AvailabilityStats([FromQuery(Name = "price-min")] string priceMin, [FromQuery(Name = "price-max")] string priceMax, [FromQuery(Name = "neighbourhood")] string neighbourhood, [FromQuery(Name = "rating-min")] string ratingMin, [FromQuery(Name = "rating-max")] string ratingMax)
        {
            Filter filter = new Filter(priceMin, priceMax, neighbourhood, ratingMin, ratingMax);

            string cacheKey = $"availabilityStats_{filter.CacheKey}-cache";
            var cacheItem = await _cache.GetStringAsync(cacheKey);

            if (cacheItem != null)
            {
                return Ok(cacheItem);
            }

            string response = JsonSerializer.Serialize(await _listingSummaryRepo.AvailabilityStats(filter), _serializerOptions);

            _cache.SetStringAsync(cacheKey, response, _cacheOptions);

            return Ok(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
