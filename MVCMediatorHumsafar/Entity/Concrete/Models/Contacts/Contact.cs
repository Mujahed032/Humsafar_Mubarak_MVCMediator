using Entity.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Concrete.Models.Contacts;

public class Contact : Common.Entity
{
    public Guid ContactGuidId { get; set; } 
    public string? PhoneCountryCode { get; set; } = "+91";
    public required string PhNumber { get; set; }
    [EmailAddress]
    public string? EmailID { get; set; }

    public Address? Address { get; set; }
    public Guid CandidateGuidId { get; set; }
    public int CandidateId { get; set; }

    public string PhoneNumber()  => PhoneCountryCode + "-" + PhNumber;

}
