using System.ComponentModel.DataAnnotations;


namespace ModelsLibrary.DTOs
{
    public class RecipientDTO
    {
        [Required]
        [RegularExpression("^\\p{L}+$", ErrorMessage = "{0} should only contain alphabetical letters and no spaces.")]
        [Display(Name = "Recipient's first name")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression("^\\p{L}+$", ErrorMessage = "{0} should only contain alphabetical letters and no spaces.")]
        [Display(Name = "Recipient's last name")]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^[\\p{L}0-9.,\\-/-: ]+$", ErrorMessage = "{0} should contain alphabetical letters, numbers, spaces and symbols '.,/:'")]
        [Display(Name = "Recipient's Adress")]
        public string? Address { get; set; }
        [Required]
        [Display(Name = "Recipient's phone number")]
        [RegularExpression("^[+]?[0-9]+$", ErrorMessage = "{0} should contain only digits and/or a '+' symbol at the begining")]
        [MinLength(7, ErrorMessage = "{0} must contain at least 7 digits")]
        [MaxLength(15, ErrorMessage = "{0} must not contain more than 15 digits")]
        public string? Phone { get; set; }
    }
}
