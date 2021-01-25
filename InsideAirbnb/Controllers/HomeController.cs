using InsideAirbnb.Models;
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
        private readonly ILogger<HomeController> _logger;
        private readonly AirBNBContext _context;


        public HomeController(ILogger<HomeController> logger, AirBNBContext context)
        {
            _logger = logger;
            _context = context;
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
            IQueryable<ListingSimpleViewModel> listingsQuery = _context.Listings
                .Where(listings => listings.Latitude != null && listings.Longitude != null && listings.City == "Amsterdam")
                .Select(l =>
                    new ListingSimpleViewModel
                    {
                        Id = l.Id,
                        HostName = l.HostName,
                        Name = l.Name,
                        Neighbourhood = l.Neighbourhood,
                        NeighbourhoodCompact = l.NeighbourhoodCleansed,
                        City = l.City,
                        RoomType = l.RoomType,
                        Price = l.Price,
                        MinimumNights = l.MinimumNights,
                        MaximumNights = l.MaximumNights,
                        Rating = l.ReviewScoresRating,
                        NumberOfReviews = l.NumberOfReviews,
                        LastReview = l.LastReview,
                        Latitude = AmsterdamDBLatitude(l.Latitude),
                        Longitude = AmsterdamDBLongitude(l.Longitude)
                    }
                );

            if (neighbourhood != null && neighbourhood.Length > 0)
            {
                listingsQuery = listingsQuery.Where(listing => listing.NeighbourhoodCompact == neighbourhood);
            }

            if (ratingMin != null && ratingMin.Length > 0)
            {
                listingsQuery = listingsQuery.Where(listing => listing.Rating >= (int.Parse(ratingMin) * 10));
            }

            if (ratingMax != null && ratingMax.Length > 0)
            {
                listingsQuery = listingsQuery.Where(listing => listing.Rating <= (int.Parse(ratingMax) * 10));
            }

            List<ListingSimpleViewModel> listings = listingsQuery.Take(1000).ToList();

            return Ok(listings);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static double? AmsterdamDBLatitude(double? dbLatitude)
        {
            if (dbLatitude == null) return null;

            string latitudeString = dbLatitude.ToString();

            double first = double.Parse($"{latitudeString[0]}{latitudeString[1]}");
            double decimals =  double.Parse($"0.{latitudeString.Substring(2, 6)}", CultureInfo.InvariantCulture);

            return first + decimals;
        }

        private static double? AmsterdamDBLongitude(double? dbLongitude)
        {
            if(dbLongitude == null) return null;

            string longitudeString = dbLongitude.ToString();

            double first = double.Parse($"{longitudeString[0]}");
            double decimals = double.Parse($"0.{longitudeString.Substring(1, 6)}", CultureInfo.InvariantCulture);

            return first + decimals;
        }
    }
}
