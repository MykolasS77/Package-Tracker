using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class RecipientInformation
    {
        public int Id { get; set; }
        
        [Required]
        [RegularExpression("^[A-ZĄČĘĖĮŠŲŪŽa-ząčęėįšųūž]+$")]
        public string? FirstName { get; set; } 
        [Required]
        [RegularExpression("^[A-ZĄČĘĖĮŠŲŪŽa-ząčęėįšųūž]+$")] 
        public string? LastName { get; set; } 
        [Required]
        [RegularExpression("^[A-ZĄČĘĖĮŠŲŪŽa-ząčęėįšųūž0-9.,/-:]+$")]
        public string? Address { get; set; } 
        [Required]
        [RegularExpression("^[+]?[0-9]+$", ErrorMessage = "The recipient's phone number should contain only digits and/or a '+' symbol at the begining")]
        public string? Phone { get; set; }
        
        public int PackageRef { get; set; }
        
        [JsonIgnore]
        public PackageInformation? Package { get; set; }
    }
}
