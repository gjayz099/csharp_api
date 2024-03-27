using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using api_relation.Models;

namespace api_relation.Dtos
{
    public class FriendDTo
    {
        // public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public WorkDto Work { get; set;} 
        public List<CarDto> Car { get; set;}
        public List<ThingsDto> Things { get; set;}

    }
}