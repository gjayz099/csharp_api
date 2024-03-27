using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_relation.Models
{
    public class Things
    {
        public int Id { get; set; }
        public string ThingsName { get; set; }
        [JsonIgnore]
        public List<Friend> Friends { get; set; }

    }
}