using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DronePackageDelivery.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Drone : IPrototype<Drone>
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public int weight { get; set; }
        public Drone()
        {

        }

        public Drone ShallowCopy()
        {
            return (Drone)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
