using Application.Interfaces.Persistence;
using Domain.Concrete.Models.Enum;
using MediatR;

namespace Application.Candidates.Queries.ListOfCandidates.GetDetailById
{
    public static class GetDetailById
    {
        public record Query(Guid Id) : IRequest<CandidateDetailModel>;

        public record CandidateDetailModel
        (
            string Name,
            int Age,
            MaritalStatus MaritalStatus,
            Gender Gender,
            string Country,
            string State,
            string City,
            string ZipPostalCode,
            string PhotoUrl
        );

        public class Handler : IRequestHandler<Query, CandidateDetailModel>
        {
            private readonly ICandidateRepository _repository;

            public Handler(ICandidateRepository repository)
            {
                _repository = repository;
            }

            public async Task<CandidateDetailModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var candidate = await _repository.GetAsync(request.Id);

                var modelToReturn = new CandidateDetailModel(
                    $"{candidate.FirstName} {candidate.LastName}",
                    candidate.Age,
                    candidate.MaritalStatus,
                    candidate.Gender,
                    candidate.Contact?.Address.Country ?? "",
                    candidate.Contact?.Address.State ?? "",
                    candidate.Contact?.Address.City ?? "",
                    candidate.Contact?.Address.ZipPostalCode ?? "",
                     candidate.Photos.FirstOrDefault(p => p.IsMain)?.Url ?? string.Empty
                    );

                return modelToReturn;
            }
        }
    }
}
