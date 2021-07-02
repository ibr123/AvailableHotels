using Microsoft.AspNetCore.Mvc;
using System;
using AppServices.Providers;

namespace AvailableHotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsDataController : ControllerBase
    {
        ProviderServices providersServices;

        public HotelsDataController()
        {
            providersServices = new ProviderServices();
        }

        [Route("AvailableHotel/{fromDate}/{toDate}/{city}/{numberOfAdults}")]
        [HttpGet]
        public string AvailableHotel(DateTime fromDate, DateTime toDate, string city, int numberOfAdults)
        {
            try
            {
                ///current path is taken from an injected object at the class "Startup"
                string results = providersServices.GetAvailableHotelByData(fromDate, toDate, city, numberOfAdults, Startup.currentPath);
                return results;
            }
            catch (Exception ex)
            {
                return "An exception occured, exception message is: " + ex.Message;
            }
        }        
    }
}
