using AppServices.Providers;
using AvailableHotels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AvailableHotels.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ILogger<HotelsController> _logger;
        ProviderServices providersServices;

        public HotelsController(ILogger<HotelsController> logger)
        {
            _logger = logger;
            providersServices = new ProviderServices();
        }

        public IActionResult Index(DateTime fromDate, DateTime toDate, string city, int numberOfAdults)
        {
            ///current path is taken from an injected object at the class "Startup"
            string results = providersServices.GetAvailableHotelByData(fromDate, toDate, city, numberOfAdults, Startup.currentPath);

            HotelsData hotelsDate = new HotelsData()
            {
                fromDate = fromDate,
                toDate = toDate,
                city = city,
                numberOfAdults = numberOfAdults
            };

            return View(hotelsDate);
        }

        /// <summary>
        /// can be called by Ajax request to update the page with the Hotels data
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="city"></param>
        /// <param name="numberOfAdults"></param>
        /// <returns>returns json object, null in case of an exception</returns>
        [HttpGet]
        public IActionResult GetHotelsData(DateTime fromDate, DateTime toDate, string city, int numberOfAdults)
        {
            try
            {
                string results = providersServices.GetAvailableHotelByData(fromDate, toDate, city, numberOfAdults, Startup.currentPath);
                return Content(results);
            }
            catch(Exception ex)
            {
                return null;
            }
        }        
    }
}
