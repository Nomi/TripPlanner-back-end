using TripPlannerAPI.Models;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Data;

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
            return await appDbContext.Trips.FirstOrDefaultAsync(t => t.tripId == id);
        }

        public async Task<IEnumerable<Trip>> GetTripsAsync()
        {
            return await appDbContext.Trips.ToListAsync();
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
