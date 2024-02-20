// OwnerViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace HousingSystem.ViewModels
{
    public class OwnerViewModel
    {
        [MaxLength(5)]
        public string OwnerId { get; set; }

        [Required]
        public string OwnerFirstName { get; set; }

        [Required]
        public string OwnerLastName { get; set; }

        [Required]
        [MaxLength(10)]
        [Phone]
        public string OwnerPhone { get; set; }

        [Required]
        [EmailAddress]
        public string OwnerEmailId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OwnerDateOfPurchase { get; set; }

        [Required]
        [Display(Name = "Flat Number")]
        public string FlatNo { get; set; }
    }
}
