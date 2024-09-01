using Entity.Concrete.Models.Candidates;
using MVC_Humsafar_Mubarak.Enum;

namespace MVC_Humsafar_Mubarak.Interface
{
    public interface IProfileRepository
    {


        //Task<CandidateProfile> GetByIdAsync(int id);
        //Task<CandidateProfile> GetByIdAsyncNoTracking(int id);
        //Task<bool> AddAsync(CandidateProfile profile);

        //Task<bool> UpdateAsync(CandidateProfile profile);

        //Task<bool> DeleteAsync(CandidateProfile profile);

        //Task<bool> SaveAsync();
        Task<IEnumerable<Candidate>> GetAllProfileAsync();
        Task<Candidate> GetProfileByUserId(string userId);
        Task<Candidate> GetProfileByIdAsNoTracking(int id);
        Task<IEnumerable<Candidate>> GetProfilesByOppositeGender(Gender? gender);
        Task<bool> AddAsync(Candidate profile);
        Task<bool> UpdateProfileAsync(Candidate profile);
        Task<bool> DeleteAsync(Candidate profile);

    }
}
