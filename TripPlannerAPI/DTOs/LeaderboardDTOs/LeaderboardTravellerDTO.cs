namespace TripPlannerAPI.DTOs.LeaderboardDTOs
{
    public class LeaderboardTravellerDTO
    {
        public string username { get; set; }
        public float distance { get; set; }
        public int numTripsJoined { get; set; }
        public int numTripsCreated { get; set; }
        public float UserRating { get; set; }
        public float OrganizerRating { get; set; }
    }
}
