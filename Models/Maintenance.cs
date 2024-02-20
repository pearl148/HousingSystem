
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;
public class Maintenance
{   [Key]   
    [MaxLength(12)]  
    [Column(TypeName = "varchar(12)")]
    public string MaintenanceId{get; set; } 
    
    [Required] 
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime MaintenanceMonthYear{ get; set; }
   
    [Required] 
    public string MaintenanceAccountHead{ get; set; }
    [Required]
    public float MaintenanceAmount{ get; set; }
    [Required]
    public string MaintenanceRemark{ get; set; }
    
    //foreign key
    
    [MaxLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string FlatNo { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string OccupantId { get; set; }

        // Other Occupant properties...

        // Navigation properties
        public Flat? Flat { get; set; }
        public Occupant? Occupant { get; set; }
    
    
    
}