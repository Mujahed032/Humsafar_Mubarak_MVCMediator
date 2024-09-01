using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Qualification;

public class Education
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public EducationLevel? Standard { get; set; }
    public string? NameOfInstitution { get; set; }
    public string? Grade { get; set; }
    
    public int MoreDetailId { get; set; }
    
    public Guid MoreDetailGuidId { get; set; }
}
