
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;
public class Expense
{   [Key]   
    public int ExpenseId{get; set; } 
    [Required] 
    public string ExpenseItem{ get; set; }
    [Required] 
    [DataType(DataType.Date)]
    public DateTime ExpenseDate{ get; set; }
    
    [Required] 
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ExpenseMonthYear{ get; set; }
    [Required]
    public float ExpenseAmount{ get; set; }
    [Required] 
    public string ExpenseAccountHead{ get; set; }
   
    [Required]
    public string ExpenseRemark{ get; set; }

    [Display(Name = "Expense Proof file")]
    [NotMapped] // Exclude from database mapping
    public IFormFile? ExpenseProofFile { get; set; }

   
    [Display(Name = "Expense Proof data")]
    public byte[]? ExpenseProofData { get; set; }
    
    
  
    
}