//using Application.Interfaces.Persistence;

//namespace Application.Candidates.Queries.ListOfCandidates.GetContact
//{
//    public static class GetAllContactListMediatR
//    {

//        public record ContactModel
//            (
//                int Id,
//                string Phone,
//                string Email,
//                string City

//            );

//        public static List<ContactModel> Execute(ICandidateRepository candidateRepository)
//        {

//            var candidates = candidateRepository.GetAll()
//                .Select(c => new ContactModel
//                (
//                 c.Contact.Id,
//                c.Contact.PhoneNumber(),
//                 c.Contact.EmailID,
//                 c.Contact.Address.City
//                ));

//            return candidates.ToList();
//        }
//    }
//}