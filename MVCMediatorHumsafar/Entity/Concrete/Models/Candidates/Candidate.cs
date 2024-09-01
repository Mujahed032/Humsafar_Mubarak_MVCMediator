using Domain.Concrete.Models.Enum;
using Entity.Concrete.Models.Contacts;
using Entity.Concrete.Models.Person;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Candidates;


public class Candidate : Common.Entity
{
    public Guid CandidateGuidId { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required int HeightInInches { get; set; }
    public required int HeightInFeet { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public MoreDetail? MoreDetails { get; set; }
    public Contact? Contact { get; set; } 
    public List<Photo>? Photos { get; set; }   
    public Guid AppUserId { get; set; }
    public int Age => GetMyAge();
    private int GetMyAge()
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;

        // Adjust age if the birthday hasn't occurred yet this year
        if (DateOfBirth.Date > today.AddYears(-age)) age--;

        return age;
    }   

}
