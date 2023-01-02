using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Data;
using TripPlannerAPI.DTOs.LeaderboardDTOs;
using TripPlannerAPI.DTOs.PostDTOs;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public interface ILeaderboardRepository
    {
        public Task<IEnumerable<LeaderboardTravellerDTO>> GetLeaderboard(string type);
    }
}
