using TripPlannerAPI.Models;

namespace TripPlannerAPI.DTOs.PostDTOs
{
    public class PostDTO
    {
        public PostDTO(Post post)
        {
            creatorId = post.Creator.Id;
            creatorUsername = post.Creator.UserName;
            content = post.Content;
            postId = post.postId;
            tripId = post.RelatedTrip.tripId;
            CreationDateTime = post.CreationDateTime;
        }

        public string creatorId { get; set; }
        public string creatorUsername { get; set; }
        public string content { get; set; }
        public int postId { get; set; }
        public int tripId { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
