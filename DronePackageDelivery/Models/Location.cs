using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronePackageDelivery.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Location
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public int weight { get; set; }
        public Location()
        {

        }

        public override bool Equals(object? obj)
        {
            var obj2 = obj as Location;

            if (this == null && obj2 == null)
            {
                return true;
            }

            if ((this == null && obj2 != null) || (this != null && obj2 == null))
            {
                return false;
            }

            return name == obj2?.name && weight == obj2?.weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, weight);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
