using TripPlannerAPI.Models;

namespace TripPlannerAPI.Controllers
{
    public class TripInputDto
    {
        public DateTime date { get; set; } 
        public string startTime { get; set; }
        public float totalTime { get; set; }
        public String description { get; set; }
        public float distance { get; set; }
        public String type { get; set; } 
        public List<String> preferences { get; set; }
        public List<Location> waypoints { get; set; }
        public List<Pin> pins { get; set; }
    }
}
