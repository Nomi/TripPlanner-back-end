using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public interface ITripRepository
    {
        public Task<Trip> GetTripAsync(int id);
        public Task<IEnumerable<Trip>> GetTripsNotMemberOrCreatorAsync(User usr);
        public Task<IEnumerable<Trip>> GetTripsAsync();
        public Task<Trip> CreateTripAsync(Trip trip);
        public Task<Trip> UpdateTripAsync(Trip trip);
        public void DeleteTripAsync(int id);
    }
}