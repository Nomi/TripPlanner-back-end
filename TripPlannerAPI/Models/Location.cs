using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace TripPlannerAPI.Models
{
    public class Location
    {
        [Key] 
        public int locationId { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public string? name { get; set; }
    }
}
