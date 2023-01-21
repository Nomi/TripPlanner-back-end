using TripPlannerAPI.Models;

namespace TripPlannerAPI.DTOs
{
    public class PinDto
    {
        public int TripId { get; set; }
        public List<Pin> Pins { get; set; }
    }
}
