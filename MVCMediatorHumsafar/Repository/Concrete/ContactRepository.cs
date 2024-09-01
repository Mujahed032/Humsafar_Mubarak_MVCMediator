using Application.Interfaces.Persistence;
using Entity.Concrete.Models.Contacts;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Concrete
{
    public class ContactRepository : IContactRepository
    {
        private readonly APIDbContext _context;

        public ContactRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<Contact> GetContactByCandidateId(Guid candidateId)
        {
            var result = await _context.Candidates.Include(c => c.Contact).FirstOrDefaultAsync(c => c.CandidateGuidId == candidateId);
            return result.Contact;
        }
    }
}