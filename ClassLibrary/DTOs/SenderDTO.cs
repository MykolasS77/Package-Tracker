using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DTOs
{
    public class SenderDTO
    {
        [Required]
        [RegularExpression("^\\p{L}+$", ErrorMessage = "{0} should only contain alphabetical letters and no spaces.")]
        [Display(Name = "Sender's first name")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression("^\\p{L}+$", ErrorMessage = "{0} should only contain alphabetical letters and no spaces.")]
        [Display(Name = "Sender's last name")]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^[\\p{L}0-9.,\\-/-: ]+$", ErrorMessage = "{0} should contain alphabetical letters, numbers, spaces and symbols '.,/:'")]
        [Display(Name = "Sender's Adress")]
        public string? Address { get; set; }
        [Required]
        [Display(Name = "Sender's phone number")]
        [RegularExpression("^[+]?[0-9]+$", ErrorMessage = "{0} should contain only digits and/or a '+' symbol at the begining with no spaces")]
        [MinLength(7, ErrorMessage = "{0} must contain at least 7 digits")]
        [MaxLength(15, ErrorMessage = "{0} must not contain more than 15 digits")]
        public string? Phone { get; set; }

    }
}
