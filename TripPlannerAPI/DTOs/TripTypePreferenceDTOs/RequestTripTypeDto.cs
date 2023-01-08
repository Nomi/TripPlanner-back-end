namespace TripPlannerAPI.DTOs.TripTypePreferenceDTOs
{
    public class RequestTripTypeDto
    {
        public int TripTypeId { get; set; }
        public string? TripTypeName { get; set; }
        public List<TripTypePreferenceInputDto>? TripTypePreferences { get; set; }
    }
}
