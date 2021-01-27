using InsideAirbnb.Repositories;
using InsideAirbnb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IListingRepository _listingRepo;

        public AdminController(IListingRepository listingRepo)
        {
            _listingRepo = listingRepo;
        }

        public async Task<IActionResult> Index()
        {
            var adminChartsData = new AdminChartsDataViewModel
            {
                NeighbourhoodReviewStats = await _listingRepo.NeighbourhoodReviewStats(),
                PropertyGuestsStats = await _listingRepo.PropertyGuestsStats()
            };

            return View(adminChartsData);
        }
    }
}
