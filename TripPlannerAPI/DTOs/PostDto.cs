using TripPlannerAPI.Models;

namespace TripPlannerAPI.DTOs
{
    public class PostDTO
    {
        public PostDTO(Post post)
        {
            this.creatorId = post.Creator.Id;
            this.creatorUsername = post.Creator.UserName;
            this.content = post.Content;
            this.postId = post.postId;
            this.tripId = post.RelatedTrip.tripId;
            this.CreationDateTime = post.CreationDateTime;
        }

        public String creatorId { get; set; }
        public String creatorUsername { get; set; }
        public String content { get; set; }
        public int postId { get; set; }
        public int tripId { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
