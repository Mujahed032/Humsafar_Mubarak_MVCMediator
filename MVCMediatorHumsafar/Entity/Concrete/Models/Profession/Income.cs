using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entity.Concrete.Models.Profession;

public class Income 
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Currency { get; set; }
    public required double Amount { get; set; }
    public IncomeFrequency Frequency { get; set; }
    
    public int MoreDetailId { get; set; }
    
    public Guid MoreDetailGuidId { get; set; }

    //Read only property 
    public int MonthlySalaryInIndianRupees => calculateMonthlySalaryInRupees();

    private int calculateMonthlySalaryInRupees()
    {
        //TODO Do the calculation and return monthly salary in Rupees
        return 200000;
    }
};