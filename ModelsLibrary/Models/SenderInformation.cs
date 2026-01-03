using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class SenderInformation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} string missing in SenderInformation")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "{0} string missing in SenderInformation")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "{0} string missing in SenderInformation")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "{0} string missing in SenderInformation")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "{0} int value missing in SenderInformation")]
        public int PackageRef { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "{0} object missing in SenderInformation")]
        public PackageInformation? Package { get; set; }
    }
}
