using TripPlannerAPI.Models;

namespace TripPlannerAPI.DTOs
{
    public class PostDTO
    {
        public PostDTO(Post post)
        {
            this.creatorId = post.Creator.Id;
            this.content = post.Content;
            this.postId = post.postId;
            this.tripId = post.RelatedTrip.tripId;
        }

        public String creatorId { get; set; }
        public String content { get; set; }
        public int postId { get; set; }
        public int tripId { get; set; }
    }
}
