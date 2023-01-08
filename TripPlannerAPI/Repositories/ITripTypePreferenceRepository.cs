using TripPlannerAPI.DTOs.TripTypePreferenceDTOs;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public interface ITripTypePreferenceRepository
    {
        public Task<List<RequestTripTypeDto>> GetPreferences(User user);
        public Task UpdatePreferences(User user, RequestTripTypeDto request);
    }
}
