using TripPlannerAPI.Models;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using TripPlannerAPI.DTOs;
using System.Reflection.Metadata.Ecma335;
using System;

namespace TripPlannerAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Post> CreatePostAsync(Post post)
        {
            var result = await appDbContext.Posts.AddAsync(post);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public static PostDTO ToPostDTO(Post post)
        {
            return new PostDTO(post);
        }
        public async Task<IEnumerable<PostDTO>> GetPostsAsync(int tripId)
        {
            return await appDbContext.Posts
                .Where(x=>x.RelatedTrip.tripId==tripId)
                .Include(x => x.Creator)
                .Select(x => ToPostDTO(x))
                .ToListAsync();
        }
    }
}
