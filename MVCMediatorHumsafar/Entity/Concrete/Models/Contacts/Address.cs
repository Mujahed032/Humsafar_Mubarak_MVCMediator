using Entity.Concrete.Models.Residence;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Contacts;

public class Address
{   
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Country { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? State { get; set; }
    public required string City { get; set; }
    public string? ZipPostalCode { get; set; }
    public AddressType AddressType { get; set; }
    [ForeignKey("Contact")]
    public int ContactId { get; set; }
    [ForeignKey("Contact")]
    public Guid ContactGuidId { get; set; }
}
