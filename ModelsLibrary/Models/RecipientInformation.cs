using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class RecipientInformation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} string missing in RecipientInformation")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "{0} string missing in RecipientInformation")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "{0} string missing in RecipientInformation")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "{0} string missing in RecipientInformation")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "{0} int value missing in RecipientInformation")]
        public int PackageRef { get; set; }
        
        [JsonIgnore]
        [Required(ErrorMessage = "{0} object missing in RecipientInformation")]
        public PackageInformation? Package { get; set; }
    }
}
