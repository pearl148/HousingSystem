

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;
public class Occupant
{   [Key]       
    [MaxLength(5)]  
    [Column(TypeName = "varchar(5)")]
    public string OccupantId{get; set; } 
    [Required] 
    public bool OccupantIsOwned{ get; set; }
    [Required] 
    public string OccupantFirstName{ get; set; }
    [Required] 
    public string OccupantLastName{ get; set; }
    [Required]
    [MaxLength(10)]  
    [Phone]
    [Column(TypeName = "varchar(10)")]
    public string OccupantPhone { get; set; }
    [Required]
    [EmailAddress]
    public string OwnerEmailId{ get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime OccupantStartDate{ get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime OccupantEndDate{ get; set; }
    //foreign key
      [MaxLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string FlatNo { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string OwnerId { get; set; }

        // Other Occupant properties...

        // Navigation properties
        public Flat? Flat { get; set; }
        public Owner? Owner { get; set; }
    public User? User{ get; set; }
    public ICollection<Payment>? Payment { get; set; }
    public ICollection<Maintenance>? Maintenances { get; set; }

    
}