using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Models
{
    public class InputData
    {
        public IEnumerable<Drone> Drones { get; set; }
        public IEnumerable<Location> Locations { get; set; }

        public InputData()
        {
            Drones = new List<Drone>();
            Locations = new List<Location>();
        }
    }
}
