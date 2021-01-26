using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.ViewModels
{
    public class ListingSummaryViewModel
    {
        public int Id { get; set; }

        public string? HostName { get; set; }

        public string Name { get; set; }
        public string? Neighbourhood { get; set; }
        public string? RoomType { get; set; }

        public int? Price { get; set; }
        public int? MinimumNights { get; set; }

        public int? Rating { get; set; }
        public int? NumberOfReviews { get; set; }
        public DateTime? LastReview { get; set; }

        public int? Availability365 { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
 