using InsideAirbnb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public interface IListingSummaryRepository
    {
        public IQueryable<ListingSummaryViewModel> Filter(Filter filter);
        public List<RoomTypeStatsViewModel> RoomTypeStats(Filter filter);
        public List<AvailabilityStatsViewModel> AvailabilityStats(Filter filter);
    }
}
