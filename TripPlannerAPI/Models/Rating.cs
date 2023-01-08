using System.ComponentModel.DataAnnotations;

namespace TripPlannerAPI.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public bool IsOrganizer { get; set; }
        public float RatingPoints { get; set; }
        public User User { get; set; }
    }
}
