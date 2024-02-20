
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HousingSystem.Models;
public class Payment
{   [Key] 
    [MaxLength(14)]  
    [Column(TypeName = "varchar(14)")]  
    public string PaymentId{get; set; } 
    [Required] 
    public string PaymentAccountHead{ get; set; }
    [Required] 
    [DataType(DataType.Date)]
    public DateTime PaymentDate{ get; set; }
   
    [Required] 
    public string PaymentMode{ get; set; }
    [Required]
    public float PaymentAmount{ get; set; }
    [Required]
    public string PaymentTransactionId { get; set; }
    [Required]
    
    public string PaymentReceiptId{ get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? PaymentReceiptDate { get; set; }
    public string PaymentRemarks { get; set; }
    //foreign key
        [MaxLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string OccupantId { get; set; }

    [ForeignKey("OccupantId")]
    public Occupant? Occupant { get; set; }
    
   
   
    
    
}