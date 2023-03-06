using DronePackageDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Helpers
{
    public class Parser
    {
        public Parser()
        {
            
        }
        
        public (IEnumerable<Drone>, IEnumerable<Location>) Parse(string[] lines)
        {
            var drones = GetDrones(lines[0]);

            var locationsLines = string.Join('\n', lines.ToList().GetRange(1, lines.Length - 1));

            var locations = GetLocations(locationsLines);

            return (drones, locations);
        }

        private IEnumerable<Drone> GetDrones(string input)
        {
            var arrayOfDrones = input.Split(',');

            for (var i = 0; i < arrayOfDrones.Length; i += 2)
            {
                yield return new Drone()
                {
                    name = arrayOfDrones[i],
                    weight = int.Parse(arrayOfDrones[i + 1])
                };
            }
        }

        private IEnumerable<Location> GetLocations(string input)
        {
            var arrayOfLocations = input.Split('\n');

            for (var i = 0; i < arrayOfLocations.Length; i++)
            {
                var location = arrayOfLocations[i].Split(',');

                yield return new Location()
                {
                    name = location[0],
                    weight = int.Parse(location[1])
                };
            }
        }
    }
}
