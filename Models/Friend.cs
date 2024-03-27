using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_relation.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public Work Work { get; set; }

        public List<Cars> Cars { get; set; } 
        public List<Things> Things { get; set; }  

    }
}