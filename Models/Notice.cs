using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;
public class Notice{
   [Key]
    [Column(TypeName = "varchar(12)")]
    public string NoticeId{get; set; } 
    [Required] 
    [DataType(DataType.Date)]
    public DateTime FromDate{get; set;}
    [Required] 
    [DataType(DataType.Date)]
    public DateTime ToDate{get; set;}
    [Required]
    public string NoticeTitle{get; set;}

    [Required]
    public string NoticePara{get; set;}

}