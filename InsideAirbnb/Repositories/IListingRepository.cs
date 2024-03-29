﻿using InsideAirbnb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public interface IListingRepository
    {
        public Task<List<NeighbourhoodReviewStatsViewModel>> NeighbourhoodReviewStats();
        public Task<List<PropertyGuestsStatsViewModel>> PropertyGuestsStats();
    }
}
