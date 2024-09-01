using Entity.Concrete.Models.Candidates;
using Microsoft.EntityFrameworkCore;
using MVC_Humsafar_Mubarak.Data;
using MVC_Humsafar_Mubarak.Interface;
using MVC_Humsafar_Mubarak.Models;

namespace MVC_Humsafar_Mubarak.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
      

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
           
        }
        //public async Task<CandidateProfile> UserProfile()
        //{

        //    var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return null;
        //    }
        //    var userProfiles = await _context.CandidateProfile
        //        .Where(r => r.AppUserId == userId).FirstOrDefaultAsync();

        //    return userProfiles;
        //}

        //public async Task<CandidateProfile> GetByIdAsync(int id)
        //{

        //    if(_httpContextAccessor.HttpContext == null)
        //    {
        //        throw new InvalidOperationException("The HTTP context is null.");
        //    }
        //    // Get the current user ID from the HTTP context
        //    var userId = _httpContextAccessor.HttpContext?.User.GetUserId();

        //    // Return null if the user ID is not available
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return null;
        //    }

        //    // Retrieve the profile by ID and ensure it belongs to the current user
        //    var candidateProfile =  await _context.CandidateProfile.Include(p => p.Address)
        //        .FirstOrDefaultAsync(p => p.Id == id && p.AppUserId == userId);

        //    // Return the profile if found, or null if not found
        //    return candidateProfile;
        //}

        ////public async Task<CandidateProfile> GetByIdAsyncNoTracking(int id)
        ////{

        ////    if (_httpContextAccessor.HttpContext == null)
        ////    {
        ////        throw new InvalidOperationException("The HTTP context is null.");
        ////    }
        ////    // Get the current user ID from the HTTP context
        ////    var userId = _httpContextAccessor.HttpContext?.User.GetUserId();

        ////    // Return null if the user ID is not available
        ////    if (string.IsNullOrEmpty(userId))
        ////    {
        ////        return null;
        ////    }

        ////    // Retrieve the profile by ID and ensure it belongs to the current user
        ////    var candidateProfile = await _context.CandidateProfile.Include(p => p.Address).AsNoTracking()
        ////        .FirstOrDefaultAsync(p => p.Id == id && p.AppUserId == userId);

        ////    // Return the profile if found, or null if not found
        ////    return candidateProfile;
        ////}

        //public async Task<bool> SaveAsync()
        //{
        //    var saved = await _context.SaveChangesAsync();
        //    return saved > 0 ? true : false;
        //}



        //public async Task<bool> UpdateAsync(CandidateProfile profile)
        //{
        //    _context.CandidateProfile.Update(profile);
        //    return await SaveAsync();
        //}

        public async Task<Candidate> GetProfileByUserId(Guid userId)
        {
            return await _context.Candidates.FirstOrDefaultAsync(p => p.AppUserId == userId);
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await _context.Candidates.FindAsync(id);
        }

        public async Task<Candidate> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Candidates.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(Candidate profile)
        {
            _context.Candidates.Update(profile);
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
