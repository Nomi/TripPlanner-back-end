using TripPlannerAPI.Models;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace TripPlannerAPI.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext appDbContext;

        public TripRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            var result = await appDbContext.Trips.AddAsync(trip);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteTripAsync(int id)
        {
            var result = await appDbContext.Trips
                .FirstOrDefaultAsync(t => t.tripId == id);
            if (result != null)
            {
                appDbContext.Trips.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Trip> GetTripAsync(int id)
        {
            return await appDbContext.Trips.Where(t=>t.tripId == id).Include(x => x.creator).Include(x => x.members).Include(x => x.waypoints).Include(x=>x.preferences).FirstOrDefaultAsync(t => t.tripId == id);
        }
        public async Task<IEnumerable<Trip>> GetTripsAsync()
        {
            return await appDbContext.Trips.Include(x => x.creator).Include(x => x.members).Include(x => x.waypoints).Include(x => x.preferences).ToListAsync();
        }
        public async Task<IEnumerable<Trip>> GetTripsQueryParamFilteredAsync(string relationship, string timeperiod, User usr)
        {
            Expression<Func<Trip, bool>> isRelated = (x => x.tripId !=-1);
            Expression<Func<Trip, bool>> isFromTimePeriod = (x => x.tripId != -1);
            if (relationship == "created")
            {
                isRelated = (t => (t.creator.Id == usr.Id));
            }
            else
            {
                isRelated = (t => t.members.Any(u => u.Id == usr.Id));
            }
            if(timeperiod=="future")
            {
                isFromTimePeriod = (t => t.date > DateTime.Now);
            }
            else
            {
                isFromTimePeriod = (t => t.date <= DateTime.Now);
            }
            return await appDbContext.Trips.Where(isRelated).Where(isFromTimePeriod)
                .Include(x => x.creator).Include(x => x.members).Include(x => x.waypoints).Include(x => x.preferences)
                .ToListAsync();
        }
        public async Task<IEnumerable<Trip>> GetTripsNotMemberOrCreatorAsync(User usr)
        {
            return await appDbContext.Trips.Where(t => (t.creator.Id != usr.Id && !t.members.Any(u => u.Id == usr.Id)))
                .Include(x=>x.creator).Include(x=>x.members).Include(x=>x.waypoints).Include(x => x.preferences).ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetFavoriteTrips(User usr)
        {
            User usrWithFavorites = await appDbContext.Users.Where(u => u.Id == usr.Id).Include(u=> (u as User).FavoriteTrips).FirstAsync();
            return usrWithFavorites.FavoriteTrips;
        }

        public async Task<Trip> UpdateTripAsync(Trip trip)
        {
            var result = await appDbContext.Trips
                            .FirstOrDefaultAsync(t => t.tripId == trip.tripId);
            if (result != null)
            {
                result.members = trip.members;
                result.date= trip.date;
                result.preferences = trip.preferences;
                await appDbContext.SaveChangesAsync();

                return result;
            }
            
            return null;
        }
    }
}
