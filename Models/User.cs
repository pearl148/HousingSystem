
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;

public class User : IdentityUser
{
    [Required]
    public string UserRole { get; set; }

    [Required]
    public bool UserActive { get; set; }

    // Additional properties as needed...
    public string OccupantId { get; set; }
    // Foreign key
   
    [ForeignKey("OccupantId")]
    public Occupant? Occupant { get; set; }
}
