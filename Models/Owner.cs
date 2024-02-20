using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingSystem.Models
{
    public class Owner
    {
        [Key]
        [MaxLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string OwnerId { get; set; }

        [Required]
        public string OwnerFirstName { get; set; }

        [Required]
        public string OwnerLastName { get; set; }

        [Required]
        [MaxLength(10)]
        [Phone]
        [Column(TypeName = "varchar(10)")]
        public string OwnerPhone { get; set; }

        [Required]
        [EmailAddress]
        public string OwnerEmailId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OwnerDateOfPurchase { get; set; }

        [Required]
        public string FlatNo { get; set; }

        [ForeignKey("FlatNo")]
        public Flat? Flat { get; set; }

        public ICollection<Occupant>? Occupants { get; set; }
    }
}