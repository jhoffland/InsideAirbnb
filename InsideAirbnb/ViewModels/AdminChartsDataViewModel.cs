using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.ViewModels
{
    public class AdminChartsDataViewModel
    {
        public List<NeighbourhoodReviewStatsViewModel> NeighbourhoodReviewStats { get; set; }
        public List<PropertyGuestsStatsViewModel> PropertyGuestsStats { get; set; }
    }
}
