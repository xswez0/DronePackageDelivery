using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Trip
    {
        [JsonProperty(PropertyName = "drone")]
        public Drone? drone { get; set; }
        [JsonProperty(PropertyName = "locations")]
        public IEnumerable<Location>? locations { get; set; }
        [JsonProperty(PropertyName = "totalTrips")]
        public int totalTrips { get; set; }
        public Trip()
        {
            locations = new List<Location>();
        }
    }
}
