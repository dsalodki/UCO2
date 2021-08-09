using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UCO2.Models;

namespace UCO2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View(new ViewModel());
        }

        [HttpPost]
        public ActionResult Index(ViewModel model)
        {
            var service = new PaymentService(new InputParams { CountPeople = model.PlayersNumber, InitAmount = model.StartAmount });
            var result = service.Start().Result;
            ViewBag.Result = result;
            return View(new ViewModel());
        }
    }
}
