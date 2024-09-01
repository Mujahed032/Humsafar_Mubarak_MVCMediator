using Entity.Concrete.Models.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Persistence
{
    public interface IContactRepository
    {
        Task<Contact> GetContactByCandidateId(Guid candidateId);
    }
}
