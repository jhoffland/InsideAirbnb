using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.ViewModels
{
    public class ListingSimpleViewModel
    {
        public int Id { get; set; }

        public string? HostName { get; set; }

        public string Name { get; set; }
        public string? Neighbourhood { get; set; }
        public string? NeighbourhoodCompact { get; set; }
        public string? City { get; set; }
        public string? RoomType { get; set; }

        public string? Price { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }

        public int? Rating { get; set; }
        public int? NumberOfReviews { get; set; }
        public DateTime? LastReview { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
 