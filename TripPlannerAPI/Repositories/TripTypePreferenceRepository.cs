using Microsoft.EntityFrameworkCore;
using TripPlannerAPI.Data;
using TripPlannerAPI.DTOs.TripTypePreferenceDTOs;
using TripPlannerAPI.Models;

namespace TripPlannerAPI.Repositories
{
    public class TripTypePreferenceRepository : ITripTypePreferenceRepository
    {
        private readonly AppDbContext _appDbContext;

        public TripTypePreferenceRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            
        }

        public async Task<List<RequestTripTypeDto>> GetPreferences(User user)
        {
            return await _appDbContext.TripTypesPreferences
                .Where(t => t.User.Id == user.Id)
                .Include(t => t.TripType)
                .Include(t => t.PreferenceType)
                .GroupBy(t => t.TripType)
                .Select(t => new RequestTripTypeDto
                {
                    TripTypeId = t.Key.Id,
                    TripTypeName = t.Key.TypeName,
                    TripTypePreferences = t.Select(tt => new TripTypePreferenceInputDto
                    {
                        PreferenceId = tt.PreferenceType.Id,
                        PreferenceName = tt.PreferenceType.PreferenceTypeName,
                        Points = tt.Points
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task UpdatePreferences(User user, RequestTripTypeDto request)
        {
            foreach(var r in request.TripTypePreferences)
            {
                var result = await _appDbContext.TripTypesPreferences
                    .Where(t => t.User.Id == user.Id)
                    .Where(t => t.TripTypeId== request.TripTypeId)
                    .FirstOrDefaultAsync(t => t.PreferenceTypeId == r.PreferenceId);

                if (result != null)
                {
                    result.Points = r.Points;
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    await _appDbContext.TripTypesPreferences
                        .AddAsync(new TripTypePreference
                    {
                        TripTypeId = request.TripTypeId,
                        PreferenceTypeId = r.PreferenceId,
                        User = user,
                        Points = r.Points
                    });

                    await _appDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
