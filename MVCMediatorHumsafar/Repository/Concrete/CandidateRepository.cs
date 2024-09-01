using Application.Interfaces.Persistence;
using CommonX;
using Entity.Concrete.Models.Candidates;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System.Linq.Expressions;

namespace Repository.Concrete
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly APIDbContext _context;

        public CandidateRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<IResponseDataModel<IEnumerable<Candidate>>> GetAsyncWithSpecifiedFilter(Expression<Func<Candidate, bool>>? filter)
        {
            try
            {
                IQueryable<Candidate> query = _context.Candidates;

                if (filter != null)
                {
                    query = query.Where(filter);

                    // c => c.Gender != "Male"
                }

                var allCandidates = await query 
                    .ToListAsync();
                return new ResponseDataModel<IEnumerable<Candidate>>
                {
                    Success = true,
                    Data = allCandidates
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return new ResponseDataModel<IEnumerable<Candidate>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<IResponseDataModel<IEnumerable<Candidate>>> GetAllAsync()
        {
            try
            {
                IQueryable<Candidate> query = _context.Candidates;
                               
                var allCandidates = await query
                    .ToListAsync();
                return new ResponseDataModel<IEnumerable<Candidate>>
                {
                    Success = true,
                    Data = allCandidates
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return new ResponseDataModel<IEnumerable<Candidate>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            var Candidate = await _context.Candidates
                .FirstOrDefaultAsync(i => i.CandidateGuidId == id);

            return Candidate;
        }
       

        public async Task<IResponseDataModel<Candidate>> AddAsync(Candidate candidate)
        {
            try
            {
                candidate.CandidateGuidId = Guid.NewGuid();
                _context.Candidates.Add(candidate);
                return await _context.SaveChangesAsync() == 1 ?
                    new ResponseDataModel<Candidate> { Success = true, Data = candidate } :
                    new ResponseDataModel<Candidate>
                    {
                        Success = false, Message = "Didn't create the Candidate in the database through repository."
                    };

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IResponseDataModel<Candidate>> GetAsync(Expression<Func<Candidate, bool>> filter)
        {
            try
            {

                var data = await _context.Candidates
                    .SingleOrDefaultAsync(filter);
                return data != null ?
                    new ResponseDataModel<Candidate>
                    {
                        Success = true,
                        Data = data
                    } :
                    new ResponseDataModel<Candidate>
                    {
                        Success = false,
                        Message = "Candidate not found"
                    };
            }
            catch (Exception)
            {
                throw;
            }
        }


     
        public async Task<IResponseModel> UpdateAsync(Guid Id, Candidate candidateDataToUpdate)
        {
            try
            {
                var getCandidateResponse = await GetAsync(x => x.CandidateGuidId == Id);

                //If the candidate is not found, return without updating along with a message
                if (!getCandidateResponse.Success) return new ResponseModel { Success = false, Message = getCandidateResponse.Message };


                //If the candidate if found, only then the following code executes.
                var candidateQueriedFromRepository = getCandidateResponse.Data;

                candidateQueriedFromRepository.DateOfBirth = candidateDataToUpdate.DateOfBirth;
                candidateQueriedFromRepository.Gender = candidateDataToUpdate.Gender;
                candidateQueriedFromRepository.MaritalStatus = candidateDataToUpdate.MaritalStatus;

                _context.Candidates.Update(candidateQueriedFromRepository);
                return await _context.SaveChangesAsync() == 1 ?
                  new ResponseModel { Success = true } :
                  new ResponseModel
                  {
                      Success = false
                  };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseModel> RemoveAsync(Guid id)
        {
            try
            {
                var getCandidateResponse = await GetAsync(x => x.CandidateGuidId == id);
                if (!getCandidateResponse.Success) return new ResponseModel { Success = false, Message = getCandidateResponse.Message };
                _context.Candidates.Remove(getCandidateResponse.Data);
                return await _context.SaveChangesAsync() == 1 ?
                    new ResponseModel { Success = true } :
                    new ResponseModel
                    {
                        Success = false
                    };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IResponseModel> RemoveAsync(Candidate candidateToRemove)
        {
            try
            {
                
                _context.Candidates.Remove(candidateToRemove);
                return await _context.SaveChangesAsync() == 1 ?
                    new ResponseModel { Success = true } :
                    new ResponseModel
                    {
                        Success = false
                    };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IResponseDataModel<Candidate>> GetAsync(Guid Id)
        {
            var getCandidateResponse = await GetAsync(x => x.CandidateGuidId == Id);

            //If the candidate is not found, return without updating along with a message
            if (!getCandidateResponse.Success) return new ResponseDataModel<Candidate> { Success = false, Message = getCandidateResponse.Message };

            return new ResponseDataModel<Candidate>
            {
                Success = true,
                Data = getCandidateResponse.Data
            };
        }

        public IEnumerable<Candidate> GetAll()
        {
            try
            {
                IQueryable<Candidate> query = _context.Candidates
                    .Include(c => c.Photos)
                   .Include(c => c.MoreDetails)
                   .ThenInclude(md => md.Employment)
                   .Include(c => c.MoreDetails)
                   .ThenInclude(md => md.EducationDetails)
                   ;

                return query
                    .ToList();
                 
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return null;
            }
        }

        async Task<Candidate> IRepository<Candidate>.GetAsync(Guid Id)
        {
                return await _context.Candidates
                .Include(c => c.Photos)
                   .Include(c => c.MoreDetails)
                   .ThenInclude(md => md.Employment)
                   .Include(c => c.MoreDetails)
                   .ThenInclude(md => md.EducationDetails)
            .FirstOrDefaultAsync(c => c.CandidateGuidId == Id);
            
        }

      
    }
}








   
