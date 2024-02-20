using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;
public class Flat
{   [Key]   
    [MaxLength(3)]  
    [Column(TypeName = "varchar(3)")]
    [DisplayName("Flat No")]
    public string FlatNo{ get; set; }
    [Required]
    [DisplayName("Sq Feet Area")]
    public int SqFeetArea { get; set; }
   public Owner? Owner { get; set; }
    public Occupant? Occupant { get; set; }
    public ICollection<Maintenance>? Maintenance { get; set; }

    
}