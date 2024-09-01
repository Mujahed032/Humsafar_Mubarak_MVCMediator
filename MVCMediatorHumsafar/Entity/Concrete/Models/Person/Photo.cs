using Entity.Concrete.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Person;

public class Photo 
{
    [Key]
    public int Id { get; set; }
    [Required]
    public Guid PhotoGuidId { get; set; }
  
    public string? PhotoTag { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    
    public int CandidateId { get; set; }
    
    public Guid CandidateGuidId { get; set; }
}