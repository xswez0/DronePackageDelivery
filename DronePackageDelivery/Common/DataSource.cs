using DronePackageDelivery.Helpers;
using DronePackageDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Common
{
    public class DataSource : IDataSource
    {
        private Parser _parser { get; set; }
        public DataSource() {
            _parser = new Parser();
        }

        public InputData readData(string filePath)
        {
            InputData inputData = new InputData();

            string[] lines = File.ReadAllLines(filePath);

            var (drones, locations) = _parser.Parse(lines);

            inputData.Drones = drones;
            inputData.Locations = locations;

            return inputData;
        }

        public string printData(IEnumerable<Trip> trips)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var deliveriesByDrone = trips.GroupBy(x => x.drone);

            var tupleTrips = new List<(Drone?, IList<IEnumerable<Location>>)>();

            foreach (var delivery in deliveriesByDrone)
            {
                var locations = delivery.Select(x => x.locations).ToList();
                tupleTrips.Add((delivery.Key, locations));
            }

            stringBuilder.AppendLine($"Best delivery trips calculation");
            foreach (var output in tupleTrips)
            {
                stringBuilder.AppendLine(output.Item1?.ToString());
                for (int i = 1; i <= output.Item2.Count; i++)
                {
                    if (output.Item2[i - 1].Any())
                    {
                        stringBuilder.AppendLine("Trip #" + i);
                        stringBuilder.AppendLine(string.Join(", ", output.Item2[i - 1]));
                    }
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
