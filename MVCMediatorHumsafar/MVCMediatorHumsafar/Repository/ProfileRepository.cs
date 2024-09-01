//using Microsoft.EntityFrameworkCore;
//using MVC_Humsafar_Mubarak.Data;
//using MVC_Humsafar_Mubarak.Enum;
//using MVC_Humsafar_Mubarak.Interface;
//using MVC_Humsafar_Mubarak.Models;

//namespace MVC_Humsafar_Mubarak.Repository
//{
//    public class ProfileRepository : IProfileRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public ProfileRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        //public async Task<IEnumerable<CandidateProfile>> GetAllAsync()
//        //{
//        //    return await _context.CandidateProfile.ToListAsync();
//        //}

//        //public async Task<CandidateProfile> GetByIdAsync(int id)
//        //{
//        //    return await _context.CandidateProfile.Include(a => a.Address).FirstOrDefaultAsync(i => i.Id == id);
//        //}

//        //public async Task<bool> AddAsync(CandidateProfile profile)
//        //{
//        //    await _context.CandidateProfile.AddAsync(profile);
//        //    return await SaveAsync();
//        //}

//        //public async Task<bool> UpdateAsync(CandidateProfile profile)
//        //{
//        //    _context.CandidateProfile.Update(profile);
//        //    return await SaveAsync();
//        //}

//        //public async Task<bool> DeleteAsync(CandidateProfile profile)
//        //{
//        //    _context.CandidateProfile.Remove(profile);
//        //    return await SaveAsync();
//        //}

//        //public async Task<bool> SaveAsync()
//        //{
//        //    return await _context.SaveChangesAsync() > 0;
//        //}

//        //public async Task<CandidateProfile> GetByIdAsyncNoTracking(int id)
//        //{
//        //    return await _context.CandidateProfile.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
//        //}


//        public async Task<Candidate> GetProfileByUserId(string userId)
//        {
//            return await _context.CandidateProfile.Include(a => a.Address).FirstOrDefaultAsync(p => p.AppUserId == userId);
//        }

//        public async Task<IEnumerable<Candidate>> GetProfilesByOppositeGender(Gender? gender)
//        {
//            return await _context.CandidateProfile
//                .Where(p => p.Gender != gender)
//                .ToListAsync();
//        }

//        public async Task<bool> AddAsync(Candidate profile)
//        {
//            await _context.CandidateProfile.AddAsync(profile);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> UpdateProfileAsync(Candidate profile)
//        {
//            _context.CandidateProfile.Update(profile);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> DeleteAsync(Candidate profile)
//        {
//            _context.CandidateProfile.Remove(profile);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<Candidate> GetProfileByIdAsNoTracking(int id)
//        {
//            return await _context.CandidateProfile.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                 
//        }

//        public async Task<IEnumerable<Candidate>> GetAllProfileAsync()
//        {
//            return await _context.CandidateProfile.Include(p => p.Address).ToListAsync();
//        }
//    }
//}//using Microsoft.EntityFrameworkCore;
//using MVC_Humsafar_Mubarak.Data;
//using MVC_Humsafar_Mubarak.Enum;
//using MVC_Humsafar_Mubarak.Interface;
//using MVC_Humsafar_Mubarak.Models;

//namespace MVC_Humsafar_Mubarak.Repository
//{
//    public class ProfileRepository : IProfileRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public ProfileRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        //public async Task<IEnumerable<CandidateProfile>> GetAllAsync()
//        //{
//        //    return await _context.CandidateProfile.ToListAsync();
//        //}

//        //public async Task<CandidateProfile> GetByIdAsync(int id)
//        //{
//        //    return await _context.CandidateProfile.Include(a => a.Address).FirstOrDefaultAsync(i => i.Id == id);
//        //}

//        //public async Task<bool> AddAsync(CandidateProfile profile)
//        //{
//        //    await _context.CandidateProfile.AddAsync(profile);
//        //    return await SaveAsync();
//        //}

//        //public async Task<bool> UpdateAsync(CandidateProfile profile)
//        //{
//        //    _context.CandidateProfile.Update(profile);
//        //    return await SaveAsync();
//        //}

//        //public async Task<bool> DeleteAsync(CandidateProfile profile)
//        //{
//        //    _context.CandidateProfile.Remove(profile);
//        //    return await SaveAsync();
//        //}

//        //public async Task<bool> SaveAsync()
//        //{
//        //    return await _context.SaveChangesAsync() > 0;
//        //}

//        //public async Task<CandidateProfile> GetByIdAsyncNoTracking(int id)
//        //{
//        //    return await _context.CandidateProfile.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
//        //}


//        public async Task<Candidate> GetProfileByUserId(string userId)
//        {
//            return await _context.CandidateProfile.Include(a => a.Address).FirstOrDefaultAsync(p => p.AppUserId == userId);
//        }

//        public async Task<IEnumerable<Candidate>> GetProfilesByOppositeGender(Gender? gender)
//        {
//            return await _context.CandidateProfile
//                .Where(p => p.Gender != gender)
//                .ToListAsync();
//        }

//        public async Task<bool> AddAsync(Candidate profile)
//        {
//            await _context.CandidateProfile.AddAsync(profile);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> UpdateProfileAsync(Candidate profile)
//        {
//            _context.CandidateProfile.Update(profile);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<bool> DeleteAsync(Candidate profile)
//        {
//            _context.CandidateProfile.Remove(profile);
//            return await _context.SaveChangesAsync() > 0;
//        }

//        public async Task<Candidate> GetProfileByIdAsNoTracking(int id)
//        {
//            return await _context.CandidateProfile.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                 
//        }

//        public async Task<IEnumerable<Candidate>> GetAllProfileAsync()
//        {
//            return await _context.CandidateProfile.Include(p => p.Address).ToListAsync();
//        }
//    }
//}
