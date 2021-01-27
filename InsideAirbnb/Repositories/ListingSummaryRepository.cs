using InsideAirbnb.Models;
using InsideAirbnb.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public class ListingSummaryRepository : IListingSummaryRepository
    {
        private readonly AirBNBContext _context;
        public ListingSummaryRepository(AirBNBContext context)
        {
            _context = context;
        }
        public IQueryable<ListingSummaryViewModel> Filter(Filter filter)
        {
            return FilteredQuery(filter).Select(summaryListing => new ListingSummaryViewModel
            {
                Id = summaryListing.Id,
                HostName = summaryListing.HostName,
                Name = summaryListing.Name,
                Neighbourhood = summaryListing.Listing.Neighbourhood,
                RoomType = summaryListing.RoomType,
                Price = summaryListing.Price,
                MinimumNights = summaryListing.MinimumNights,
                Rating = summaryListing.Listing.ReviewScoresRating,
                NumberOfReviews = summaryListing.NumberOfReviews,
                LastReview = summaryListing.LastReview,
                Availability365 = summaryListing.Availability365,
                Latitude = ListingRepository.AmsterdamDBLatitude(summaryListing.Latitude),
                Longitude = ListingRepository.AmsterdamDBLongitude(summaryListing.Longitude)
            });
        }

        public List<RoomTypeStatsViewModel> RoomTypeStats(Filter filter)
        {
            var query = FilteredQuery(filter);

            return query.GroupBy(summaryListing => summaryListing.RoomType)
                .Select(groupedListing => new RoomTypeStatsViewModel
                {
                    RoomType = groupedListing.Key,
                    Amount = groupedListing.Count()
                }).ToList();
        }

        public List<AvailabilityStatsViewModel> AvailabilityStats(Filter filter)
        {
            var query = FilteredQuery(filter);

            query = query.Where(summaryListing => summaryListing.Availability365 != null);

            return query.GroupBy(summaryListing => summaryListing.Availability365)
                .Select(groupedListing => new AvailabilityStatsViewModel
                {
                    Availability365 = groupedListing.Key,
                    Amount = groupedListing.Count()

                }).ToList();
        }

        private IQueryable<SummaryListing> FilteredQuery(Filter filter)
        {
            var query = Query();

            if (filter.PriceMin != null) query = query.Where(summaryListing => summaryListing.Price >= filter.PriceMin);

            if (filter.PriceMax != null) query = query.Where(summaryListing => summaryListing.Price <= filter.PriceMax);

            if (filter.Neighbourhood != null) query = query.Where(summaryListing => summaryListing.Neighbourhood == filter.Neighbourhood);

            if (filter.RatingMin != null) query = query.Where(summaryListing => summaryListing.Listing.ReviewScoresRating >= (filter.RatingMin * 10));

            if (filter.RatingMax != null) query = query.Where(summaryListing => summaryListing.Listing.ReviewScoresRating <= (filter.RatingMax * 10));

            return query;
        }

        private IQueryable<SummaryListing> Query()
        {
            return _context.SummaryListings
                .Where(summaryListing => summaryListing.Latitude != null && summaryListing.Longitude != null)
                .Where(summaryListing => summaryListing.Listing.City == "Amsterdam");
        }
    }
}
