using System;

namespace AppServices.Dtos
{
    public class HotelsData
    {
        ///inputs
        public string ProviderName { get; set; }
        public string City { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NumberOfAdults { get; set; }
        ///outputs
        public string HotelName { get; set; }
        public int HotelRate { get; set; }
        public float HotelFare { get; set; }
        public string RoomAmenities { get; set; }
    }
}
