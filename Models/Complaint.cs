using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Construction;
namespace HousingSystem.Models;

public class Complaint{
    [Key]
    [Column(TypeName = "varchar(36)")]
    public string ComplaintId{get; set; } 
    [Required]
    public string ComplaintGiver{get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfComplaint{get ; set; } 
    [Required]
    public string ComplaintTitle{get ; set; }
    [Required]
    public string ComplaintCategory{get ; set;}
    [Required]
    public string ComplaintDetail{get ; set; }

    
}