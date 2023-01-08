namespace TripPlannerAPI.DTOs.RatingDtos
{
    public class RatingDto
    {
        public string UserName { get; set; }
        public float RatingPoints { get; set; }
        public bool IsOrganizer { get; set; }
    }
}
