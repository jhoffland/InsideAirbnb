using InsideAirbnb.Models;
using InsideAirbnb.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public class ListingRepository : IListingRepository
    {

        private readonly AirBNBContext _context;
        public ListingRepository(AirBNBContext context)
        {
            _context = context;
        }

        public Task<List<NeighbourhoodReviewStatsViewModel>> NeighbourhoodReviewStats()
        {
            var query = Query();

            query = query.Where(listing => listing.NeighbourhoodCleansed != null && listing.ReviewScoresLocation != null);

            return query.GroupBy(listing => listing.NeighbourhoodCleansed)
                .Select(groupedListing => new NeighbourhoodReviewStatsViewModel
                {
                    Neighbourhood = groupedListing.Key,
                    AverageLocationReviewScore = Math.Round(((double) groupedListing.Average(listing => listing.ReviewScoresLocation)), 2)
                })
                .OrderBy(stat => stat.AverageLocationReviewScore)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<PropertyGuestsStatsViewModel>> PropertyGuestsStats()
        {
            var query = Query();

            query = query.Where(listing => listing.PropertyType != null && listing.GuestsIncluded != null);

            return query.GroupBy(listing => listing.PropertyType)
                .Select(groupedListing => new PropertyGuestsStatsViewModel
                {
                    PropertyType = groupedListing.Key,
                    AverageGuestsIncluded = Math.Round(((double) groupedListing.Average(listing => listing.GuestsIncluded)))
                })
                .OrderByDescending(stat => stat.AverageGuestsIncluded)
                .Take(3)
                .AsNoTracking()
                .ToListAsync();
        }

        private IQueryable<Listing> Query()
        {
            return _context.Listings
                .Where(listing => listing.Latitude != null && listing.Longitude != null && listing.City == "Amsterdam");
        }

        public static double? AmsterdamDBLatitude(double? dbLatitude)
        {
            if (dbLatitude == null) return null;

            string latitudeString = dbLatitude.ToString();

            double first = double.Parse($"{latitudeString[0]}{latitudeString[1]}");
            double decimals = double.Parse($"0.{latitudeString.Substring(2)}", CultureInfo.InvariantCulture);

            return first + decimals;
        }

        public static double? AmsterdamDBLongitude(double? dbLongitude)
        {
            if (dbLongitude == null) return null;

            string longitudeString = dbLongitude.ToString();

            double first = double.Parse($"{longitudeString[0]}");
            double decimals = double.Parse($"0.{longitudeString.Substring(1)}", CultureInfo.InvariantCulture);

            return first + decimals;
        }
    }
}
