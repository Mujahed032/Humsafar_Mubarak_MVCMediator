using Application.Interfaces.Persistence;
using Entity.Concrete.Models.Contacts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Candidates.Queries.ListOfCandidates.GetContact
{
    public static class GetContactByCandidateId
    {
        public record Query(Guid CandidateId) : IRequest<ContactViewModel>;

        public class Handler : IRequestHandler<Query, ContactViewModel>
        {
            private readonly IContactRepository _repository;

            public Handler(IContactRepository repository)
            {
                _repository = repository;
            }


            public async Task<ContactViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var contact = await _repository.GetContactByCandidateId(request.CandidateId);
                var details = new ContactViewModel(
                    contact.ContactGuidId,
                    contact.EmailID,
                    contact.PhoneNumber(),
                    contact.Address.AddressLine1,
                    contact.Address.AddressLine2
                );
                return details;
            }
        }


        public record ContactViewModel(
            Guid ContactGuidId,
            string Email,
            string PhoneNumber,
            string AddressLine1,
            string AddressLine2);


    }
}
