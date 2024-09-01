using Application.Interfaces.Persistence;
using CommonX;
using Domain.Concrete.Models.Enum;
using Entity.Concrete.Models.Candidates;
using MediatR;

namespace Application.Candidates.Commands
{
    public static class CreateCandidate
    {


        public record CreateCandidateDTOCommand(Guid AppUserGuidId,
         string FirstName,
         string LastName,
         DateTime DateOfBirth,
         int HeightInFeet,
         int HeightInInches,
         Gender Gender,
         MaritalStatus MaritalStatus) : IRequest<Guid>;

        public class Handler : IRequestHandler<CreateCandidateDTOCommand, Guid>
        {
            private readonly ICandidateRepository _repository;

            public Handler(ICandidateRepository repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateCandidateDTOCommand createCommand, CancellationToken cancellationToken)
            {
                var candidate = new Candidate
                {
                    AppUserId = createCommand.AppUserGuidId,
                    FirstName = createCommand.FirstName,
                    LastName = createCommand.LastName,
                    DateOfBirth = createCommand.DateOfBirth,
                    HeightInFeet = createCommand.HeightInFeet,
                    HeightInInches = createCommand.HeightInInches,
                    Gender = createCommand.Gender,
                    MaritalStatus = createCommand.MaritalStatus
                };



                var response = await _repository.AddAsync(candidate);

                if (response.Success)
                {
                    return response.Data.CandidateGuidId;
                }
                else
                {
                    return Guid.Empty;
                }
              
            }

         
        }
    }
  
}