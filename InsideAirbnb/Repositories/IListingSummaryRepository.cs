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
        public Task<List<ListingSummaryViewModel>> Filter(Filter filter);
        public Task<List<RoomTypeStatsViewModel>> RoomTypeStats(Filter filter);
        public Task<List<AvailabilityStatsViewModel>> AvailabilityStats(Filter filter);
    }
}
