using InsideAirbnb.Models;
using InsideAirbnb.Repositories;
using InsideAirbnb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBNBContext _context;
        private readonly IListingSummaryRepository _listingSummaryRepo;

        public HomeController(AirBNBContext context, IListingSummaryRepository listingSummaryRepo)
        {
            _context = context;
            _listingSummaryRepo = listingSummaryRepo;
        }

        public IActionResult Index()
        {
            List<NeighbourhoodViewModel> neighbourhoods = _context.Neighbourhoods.Select(n => new NeighbourhoodViewModel
            {
                Neighbourhood = n.Neighbourhood1
            })
                .OrderBy(neighbourhoods => neighbourhoods.Neighbourhood)
                .ToList();

            return View(neighbourhoods);
        }

        public IActionResult Listings([FromQuery(Name = "price-min")] string priceMin, [FromQuery(Name = "price-max")] string priceMax, [FromQuery(Name = "neighbourhood")] string neighbourhood, [FromQuery(Name = "rating-min")] string ratingMin, [FromQuery(Name = "rating-max")] string ratingMax)
        {
            Filter filter = new Filter(priceMin, priceMax, neighbourhood, ratingMin, ratingMax);

            var summaryListingsQuery = _listingSummaryRepo.Filter(filter);
            List<ListingSummaryViewModel> listings = summaryListingsQuery.Take(1000).ToList();

            return Ok(listings);
        }

        public IActionResult RoomTypeStats([FromQuery(Name = "price-min")] string priceMin, [FromQuery(Name = "price-max")] string priceMax, [FromQuery(Name = "neighbourhood")] string neighbourhood, [FromQuery(Name = "rating-min")] string ratingMin, [FromQuery(Name = "rating-max")] string ratingMax)
        {
            Filter filter = new Filter(priceMin, priceMax, neighbourhood, ratingMin, ratingMax);

            return Ok(_listingSummaryRepo.RoomTypeStats(filter));
        }

        public IActionResult AvailabilityStats([FromQuery(Name = "price-min")] string priceMin, [FromQuery(Name = "price-max")] string priceMax, [FromQuery(Name = "neighbourhood")] string neighbourhood, [FromQuery(Name = "rating-min")] string ratingMin, [FromQuery(Name = "rating-max")] string ratingMax)
        {
            Filter filter = new Filter(priceMin, priceMax, neighbourhood, ratingMin, ratingMax);

            return Ok(_listingSummaryRepo.AvailabilityStats(filter));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
