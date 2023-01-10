using System.ComponentModel.DataAnnotations;

namespace TripPlannerAPI.Models
{
    public class Pin
    {
        [Key]
        public int Id { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string? Name { get; set; }
    }
}
