using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AppServices.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppServices.Providers
{
    public class ProviderServices
    {
        const string hotelsFileName = "Hotels.json";

        /// <summary>
        /// Filters the available hotels depending on the consumed parameters by the "AvailableHotel" API and returns the results as json array,
        /// return null in case of an exception
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="city"></param>
        /// <param name="numberOfAdults"></param>
        /// <param name="currentPath"></param>
        /// <returns>json string array</returns>
        public string GetAvailableHotelByData(DateTime fromDate, DateTime toDate, string city, int numberOfAdults, string currentPath)
        {
            try
            {
                var providers = GetHotelsData(currentPath);

                ///filter all the available hotels depending on the parameters that are consumed by the "AvailableHotel" API
                var filteredProviders = providers.Where(w => w.FromDate.Equals(fromDate) && w.ToDate.Equals(toDate) && w.City.Equals(city)
                                                        && w.NumberOfAdults.Equals(numberOfAdults)).Select(s => new
                                                        {
                                                            provider = s.ProviderName,
                                                            hotelName = s.HotelName,
                                                            fare = s.HotelFare,
                                                            amenities = s.RoomAmenities
                                                        }).ToList();

                string result = JsonConvert.SerializeObject(filteredProviders, Formatting.Indented);

                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Reads the Hotels data from a json file and returns it as an object of the type "HotelsData"
        /// return null in case of an exception
        /// </summary>
        /// <param name="currentPath"></param>
        /// <returns>HotelsData</returns>
        public List<HotelsData> GetHotelsData(string currentPath)
        {
            try
            {
                List<HotelsData> providersData;
                ///read from the json file that contains the hotels data
                using (StreamReader reader = new StreamReader(currentPath + "//" + hotelsFileName))
                {
                    string json = reader.ReadToEnd();
                    ///convert the json string into a list of objects of type HotelsData
                    providersData = JsonConvert.DeserializeObject<List<HotelsData>>(json,
                                    new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                }
                return providersData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
