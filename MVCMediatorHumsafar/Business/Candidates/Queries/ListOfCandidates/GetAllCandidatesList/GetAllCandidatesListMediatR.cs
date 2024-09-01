using Application.Interfaces.Persistence;
using CommonX;
using Domain.Concrete.Models.Enum;
using MediatR;

namespace Application.Candidates.Queries.ListOfCandidates.GetAllCandidatesList
{
    public static class GetAllCandidatesListMediatR
    {

        //Query
        public record Query() : IRequest<List<CandidateListModel>>;
        //Handler
        public class Handler(ICandidateRepository repository) :  IRequestHandler<Query, List<CandidateListModel>>
        {
            private readonly ICandidateRepository _repository = repository;

            public async Task<List<CandidateListModel>> Handle(Query request, CancellationToken cancellationToken)
            {

                var candidates =  _repository.GetAll();

                var candidateListModels = candidates
                    .Select(c => new CandidateListModel(
                        c.CandidateGuidId,
                        c.FirstName,
                        c.LastName,
                        c.Age, // Assuming Age is part of MoreDetails
                        c.HeightInInches,
                        c.HeightInFeet,
                        c.Gender,
                        c.MaritalStatus,
                        c.Photos.FirstOrDefault(p => p.IsMain)?.Url ?? string.Empty
                    ))
                    .ToList();
                return candidateListModels;
            }
        }
        //Response
        public record CandidateListModel
       (
           Guid CandidateGuidId,
           string FirstName,
           string LastName,
           int Age,
           int HeightInInches,
           int HeightInFeet,
           Gender Gender,
           MaritalStatus MaritalStatus,
           string photoUrl
       );
    }
}
