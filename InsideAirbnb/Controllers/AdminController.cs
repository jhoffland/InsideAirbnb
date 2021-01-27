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

        public IActionResult Index()
        {
            var adminChartsData = new AdminChartsDataViewModel
            {
                NeighbourhoodReviewStats = _listingRepo.NeighbourhoodReviewStats(),
                PropertyGuestsStats = _listingRepo.PropertyGuestsStats()
            };

            return View(adminChartsData);
        }
    }
}
