using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_relation.Models
{
    public class Work
    {
        public int Id { get; set; }
        public string NameWork { get; set;} = String.Empty;
        public int FriendId { get; set; }

        [JsonIgnore]
        public Friend Friend  {get; set;}  
    }
}