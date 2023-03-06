using DronePackageDelivery.Common;
using DronePackageDelivery.Helpers;
using DronePackageDelivery.Models;
using DronePackageDelivery.Service;
using Moq;
using Newtonsoft.Json;

namespace DronePackageDelivery.Test
{
    public class DroneDeliveryTest : IDisposable
    {
        private readonly IDataSource _dataSourceService;
        private readonly ITripService _tripService;
        private string _filePath = "";
        public DroneDeliveryTest()
        {
            _dataSourceService = new DataSource();
            _tripService = new TripService();
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Data\");
        }

        public void Dispose()
        {

        }

        [Fact]
        public void Test_Should_Generate_Correct_Output_For_File1()
        {
            var filePath = Path.Combine(_filePath, @"File1.txt");

            const string expectedOutput = "Best delivery trips calculation\r\n" +
              "DroneB\r\n" +
              "Trip #1\r\n" +
              "LocationL, LocationO, LocationK, LocationN, LocationC, LocationG, LocationJ\r\n" +
              "Trip #2\r\n" +
              "LocationE, LocationB\r\n\r\n" +
              "DroneA\r\n" +
              "Trip #1\r\n" +
              "LocationF\r\n" +
              "Trip #2\r\n" +
              "LocationM, LocationI, LocationH\r\n" +
              "Trip #3\r\n" +
              "LocationA\r\n" +
              "Trip #4\r\n" +
              "LocationD\r\n\r\n" +
              "DroneC\r\n" +
              "Trip #1\r\n" +
              "LocationP\r\n\r\n";

            var service = new DeliveryService(_dataSourceService, _tripService);
            var actual = service.Execute(filePath);

            Assert.Equal(expectedOutput, actual);
        }

        [Fact]
        public void Test_Should_Generate_Correct_Output_For_File2()
        {
            var filePath = Path.Combine(_filePath, @"File2.txt");

            const string expectedOutput = "Best delivery trips calculation\r\n" +
              "DroneB\r\n" +
              "Trip #1\r\n" +
              "LocationD, LocationA\r\n" +
              "Trip #2\r\n" +
              "LocationB, LocationF\r\n" +
              "Trip #3\r\n" +
              "LocationH, LocationE, LocationG, LocationJ, LocationI\r\n\r\n" +
              "DroneA\r\n" +
              "Trip #1\r\n" +
              "LocationC\r\n\r\n" +
              "DroneC\r\n\r\n";

            var service = new DeliveryService(_dataSourceService, _tripService);
            var actual = service.Execute(filePath);

            Assert.Equal(expectedOutput, actual);
        }
    }
}