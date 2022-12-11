using System.ComponentModel.DataAnnotations;

namespace TripPlannerAPI.Models
{
    public class Post
    {
        [Key]
        public int postId { get; set; }
        public User Creator { get; set; }
        public Trip RelatedTrip { get; set; }
        public String? Content { get; set; }
    }
}
