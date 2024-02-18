using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Validations.Rules;
using System.Text.Json.Serialization;

namespace TripPlannerAPI.Models
{
    public class Location
    {
        [Key]
        [JsonIgnore]
        public int locationId { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public string? name { get; set; }
    }
}
