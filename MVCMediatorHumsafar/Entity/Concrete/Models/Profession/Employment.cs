using Entity.Concrete.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Profession;

public class Employment 
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ProfessionTitle { get; set; } = String.Empty;
    public string? CompanyOrBusinessName { get; set; }
    public string? Designation { get; set; }
    public EmploymentType EmploymentType { get; set; }
    
    public int MoreDetailId { get; set; }
    
    public Guid MoreDetailGuidId { get; set; }

}
