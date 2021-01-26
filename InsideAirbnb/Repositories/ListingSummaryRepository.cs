using InsideAirbnb.Models;
using InsideAirbnb.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public class ListingSummaryRepository : IRepository<ListingSummaryViewModel>
    {
        private readonly AirBNBContext _context;
        public ListingSummaryRepository(AirBNBContext context)
        {
            _context = context;
        }
        public IQueryable<ListingSummaryViewModel> Filter(string priceMin, string priceMax, string neighbourhood, string ratingMin, string ratingMax)
        {
            var query = Query();

            int priceMinInt;
            if (priceMin != null && priceMin.Length > 0 && int.TryParse(priceMin, out priceMinInt))
            {
                query = query.Where(summaryListing => summaryListing.Price >= priceMinInt);
            }

            int priceMaxInt;
            if (priceMax != null && priceMax.Length > 0 && int.TryParse(priceMax, out priceMaxInt))
            {
                query = query.Where(summaryListing => summaryListing.Price <= priceMaxInt);
            }

            if (neighbourhood != null && neighbourhood.Length > 0)
            {
                query = query.Where(summaryListing => summaryListing.Neighbourhood == neighbourhood);
            }

            int ratingMinInt;
            if (ratingMin != null && ratingMin.Length > 0 && int.TryParse(ratingMin, out ratingMinInt))
            {
                query = query.Where(summaryListing => summaryListing.Listing.ReviewScoresRating >= (ratingMinInt * 10));
            }

            int ratingMaxInt;
            if (ratingMax != null && ratingMax.Length > 0 && int.TryParse(ratingMax, out ratingMaxInt))
            {
                query = query.Where(summaryListing => summaryListing.Listing.ReviewScoresRating <= (ratingMaxInt * 10));
            }

            return query.Select(summaryListing => new ListingSummaryViewModel
            {
                Id = summaryListing.Id,
                HostName = summaryListing.HostName,
                Name = summaryListing.Name,
                Neighbourhood = summaryListing.Neighbourhood,
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

        public Task<ListingSummaryViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        private IQueryable<SummaryListing> Query()
        {
            return _context.SummaryListings
                .Where(summaryListing => summaryListing.Latitude != null && summaryListing.Longitude != null)
                // .Join(_context.Listings, summaryListing => summaryListing.Id, listing => listing.Id, (summaryListing, listing) => summaryListing)
                .Where(summaryListing => summaryListing.Listing.City == "Amsterdam");
        }
    }
}
