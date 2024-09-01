using Entity.Concrete.Models.Candidates;

namespace MVC_Humsafar_Mubarak.Interface
{
    public interface IDashboardRepository
    {
        // Task<CandidateProfile> UserProfile();

        // Task<CandidateProfile> GetByIdAsync(int id);
        //Task<CandidateProfile> GetByIdAsyncNoTracking(int id);

        //Task<bool> UpdateAsync(CandidateProfile profile);

        //Task<bool> SaveAsync();

        Task<Candidate> GetProfileByUserId(Guid userId);
        Task<Candidate> GetByIdAsync(int id);
        Task<Candidate> GetByIdAsyncNoTracking(int id);
        Task<bool> UpdateAsync(Candidate profile);

    }
}
