using TripPlannerAPI.Models;

namespace TripPlannerAPI.DTOs.TripTypePreferenceDTOs
{
    public class TripTypeAddDto
    {
        public TripType TripType { get; set; }
        public List<AddTrypPreferenceDTo> Preferences { get; set; }
    }
}
