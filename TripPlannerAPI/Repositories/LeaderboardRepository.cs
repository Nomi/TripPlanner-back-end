using TripPlannerAPI.Models;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System;
using TripPlannerAPI.DTOs.LeaderboardDTOs;
using Microsoft.EntityFrameworkCore.Internal;

namespace TripPlannerAPI.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly AppDbContext appDbContext;

        public LeaderboardRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<LeaderboardTravellerDTO>> GetLeaderboard(string type)
        {
            /*
            -- SQL query for top 10 users by distance travelled.
            select [dbo].[AspNetUsers].username, sum([dbo].[Trips].distance) as SumDist from [dbo].[AspNetUsers] 
            join [dbo].[TripUser1] on [dbo].[AspNetUsers].Id = [dbo].[TripUser1].membersId
            join [dbo].[Trips] on [dbo].[TripUser1].TripsJoinedtripId = [dbo].[Trips].tripId
            WHERE [dbo].[Trips].date < SYSDATETIME()
            group by [dbo].[AspNetUsers].username order by SumDist desc
            -- Can't seem to be able to limit output to 10 results though.
            */
            Expression<Func<LeaderboardTravellerDTO, float>> orderByExpression;
            switch(type.ToLower())
            {
                case "distance":
                    orderByExpression = (x => x.distance);
                    break;
                case "userrating":
                    orderByExpression = (x => x.UserRating); 
                    break;
                case "organizerrating":
                    orderByExpression = (x => x.OrganizerRating);
                    break;
                case "numtripsjoined":
                    orderByExpression = (x => x.numTripsJoined);
                    break;
                case "numtripscreated":
                    orderByExpression = (x => x.numTripsCreated);
                    break;
                default:
                    throw new ArgumentException(nameof(type));
                    //break;
            }

            var result = await appDbContext.Users
                                    .Select(u => new LeaderboardTravellerDTO()
                                        {
                                            username = u.UserName,
                                            UserRating=u.UserRating,
                                            OrganizerRating=u.OrganizerRating,
                                            distance = u.TripsJoined.Where(t=>t.date<=DateTime.Now.AddDays(-1)).Sum(t => t.distance),//-1
                                            numTripsJoined = u.TripsJoined.Count(),
                                            numTripsCreated = u.CreatedTrips.Count()
                                        })
                                    .OrderByDescending(orderByExpression)
                                    .Take(10)
                                    .ToListAsync(); //ToListAsync is why we await. Otherwise it's just wrong.
            return result;
            #region StupidCommentedOutAttempts
            //List<User> users = await appDbContext.Users
            //    .Include(u => u.TripsJoined)
            //        .ThenInclude(t => t.distance)
            //    .
            //var query = from usr in appDbContext.Users join trp in appDbContext.Trips on usr.TripsJoined.


            //    var dbC = appDbContext;
            //    var query = dbC.Users.LeftJoin(dbC.Trips,
            //        usr=> usr.Id,
            //        trp=>trp.members.)



            //var query =
            //            appDbContext
            //                .Users
            //                .SelectMany(u =>
            //                    appDbContext
            //                        .Trips
            //                        .Where(t => u.TripsJoined.Any(x=>(x.tripId==t.tripId))).Sum(
            //                        .Select(
            //                            t => new { u.UserName, t.tripId }));


            //var querys = await appDbContext.Users.Join(appDbContext.Trips, u => u.Id, t => t.members, (u, t) => new LeaderboardTravellerDTO() { username = u.UserName, distance = t.distance });


            //var query =
            //            from u in appDbContext.Users
            //            from t in appDbContext.Trips.Where(x => x.members.Any(k=> k.Id == u.Id))
            //            select u.UserName, t.
            #endregion
        }
    }
}
