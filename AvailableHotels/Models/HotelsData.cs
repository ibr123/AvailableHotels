using System;

namespace AvailableHotels.Models
{
    /// <summary>
    /// Used to send the consumed parameters to the html page
    /// </summary>
    public class HotelsData
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string city { get; set; }
        public int numberOfAdults { get; set; }
    }
}
