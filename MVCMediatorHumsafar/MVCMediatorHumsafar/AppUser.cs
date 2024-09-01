using Entity.Concrete.Models.Candidates;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Humsafar_Mubarak.Models
{
    public class AppUser : IdentityUser
    {
        public Candidate CandidateProfile { get; set; }
    }
}
