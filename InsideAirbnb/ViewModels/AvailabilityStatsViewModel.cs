using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.ViewModels
{
    public class AvailabilityStatsViewModel
    {
        public int? Availability365 { get; set; }
        public int Amount { get; set; }
    }
}
