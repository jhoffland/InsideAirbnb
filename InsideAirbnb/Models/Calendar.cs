using System;
using System.Collections.Generic;

#nullable disable

namespace InsideAirbnb.Models
{
    public partial class Calendar
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public string Price { get; set; }

        public virtual Listing Listing { get; set; }
    }
}
