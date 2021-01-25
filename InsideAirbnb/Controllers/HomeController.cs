using InsideAirbnb.Models;
using InsideAirbnb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AirBNBContext _context;


        public HomeController(ILogger<HomeController> logger, AirBNBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<NeighbourhoodsViewModel> neighbourhoods = _context.Neighbourhoods.Select(n => new NeighbourhoodsViewModel
            {
                Neighbourhood = n.Neighbourhood1
            })
                .OrderBy(neighbourhoods => neighbourhoods.Neighbourhood)
                .ToList();

            return View(neighbourhoods);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
