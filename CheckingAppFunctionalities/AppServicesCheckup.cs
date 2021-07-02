using AppServices.Dtos;
using AppServices.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CheckingAppFunctionalities
{
    [TestFixture]
    public class AppServicesCheckup
    {
        string currentPath;
        ProviderServices providerServices;

        public AppServicesCheckup()
        {
            providerServices = new ProviderServices();
            currentPath = Environment.CurrentDirectory;
            currentPath = currentPath.Remove(currentPath.Length - 24);
        }

        /// <summary>
        /// This test will test the functionalities of deserializing the hotels json file and querying filtering the data
        /// the provided data should return three avilable hotels depending on hotels json file
        /// </summary>
        [Test]
        public void CheckHotelsAvailabilitiesFunctionality()
        {
            string jsonResult = providerServices.GetAvailableHotelByData(new DateTime(2020, 1, 22), new DateTime(2020, 2, 22), "London", 2, currentPath);

            var providersData = JsonConvert.DeserializeObject<List<HotelsData>>(jsonResult,
                                    new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            Assert.AreEqual(3, providersData.Count);
        }

        /// <summary>
        /// This test isn't providing json file so an exception should happen and the returned value is null
        /// </summary>
        [Test]
        public void CheckHotelsAvailabilitiesFunctionalityFailure()
        {
            string jsonResult = providerServices.GetAvailableHotelByData(new DateTime(2020, 1, 22), new DateTime(2020, 2, 22), "London", 2, null);

            Assert.IsNull(jsonResult);
        }

        /// <summary>
        /// Depending on the hotels json file, the available hotels should equal to 6 hotels
        /// </summary>
        [Test]
        public void CheckHotelsFilesDeserialization()
        {
            var availableHotels = providerServices.GetHotelsData(currentPath);

            Assert.AreEqual(6, availableHotels.Count);
        }

    }
}
