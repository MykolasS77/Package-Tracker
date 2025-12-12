using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class RecipientInformation
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^\\p{L}+$", ErrorMessage = "Recipient's first name should only contain alphabetical letters.")]
        [Display(Name = "Recipient's first name")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression("^\\p{L}+$", ErrorMessage = "Recipient's last name should only contain alphabetical letters.")]
        [Display(Name = "Recipient's last name")]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression("^[\\p{L}0-9.,/-: ]+$", ErrorMessage = "Recipient's adress should contain alphabetical letters, numbers, spaces and symbols '.,/:'")]
        [Display(Name = "Recipient's Adress")]
        public string? Address { get; set; }
        [Required]
        [Display(Name = "Recipient's phone number")]
        [RegularExpression("^[+]?[0-9]+$", ErrorMessage = "The phone number should contain only digits and/or a '+' symbol at the begining")]
        [MinLength(7, ErrorMessage = "The phone numeber must contain at least 7 digits")]
        [MaxLength(15, ErrorMessage = "The phone numeber must not contain more than 7 digits")]
        public string? Phone { get; set; }

        public int PackageRef { get; set; }
        
        [JsonIgnore]
        public PackageInformation? Package { get; set; }
    }
}
