using Entity.Concrete.Models.Profession;
using Entity.Concrete.Models.Qualification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Person;

public class MoreDetail 
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid MoreDetailGuidId { get; set; }

    public bool Employed { get; set; }
    public Employment? Employment { get; set; }
    public string? Description { get; set; }
    public string? BodyType { get; set; }
    public string? Complexion { get; set; }
    public string? Weight { get; set; }  
    public string? Diet { get; set; }
    public string? Personality { get; set; }
    public Religion? Religion { get; set; }
    public Income? Income { get; set; }
    public EducationLevel HighestQualification { get; set; }
    public List<Education>? EducationDetails { get; set; } = new List<Education>();
    
    public int CandidateId { get; set; }
    
    public Guid CandidateGuidId { get; set; }

}
