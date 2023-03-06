using DronePackageDelivery.Common;
using DronePackageDelivery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Service
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDataSource _dataSource;
        private readonly ITripService _tripService;
        public DeliveryService(IDataSource dataSource, ITripService tripService)
        {
            _dataSource = dataSource;
            _tripService = tripService;
        }

        public string Execute(string filePath)
        {
            var inputData = _dataSource.readData(filePath);

            var trips = _tripService.SearchMinimumBestTrips(inputData.Drones, inputData.Locations);

            var result = _dataSource.printData(trips);

            return result;
        }
    }
}
