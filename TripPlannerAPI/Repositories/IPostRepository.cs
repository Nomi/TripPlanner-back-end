using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Data;
using TripPlannerAPI.DTOs.PostDTOs;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public interface IPostRepository
    {
        public Task<IEnumerable<PostDTO>> GetPostsAsync(int tripId);
        public Task<Post> CreatePostAsync(Post post);
    }
}
