using ModelsLibrary.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    
    public class StatusHistory
    {
        
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} enum of PackageStatus missing in StatusHistory")]
        public PackageStatus? Status { get; set; }

        [Required(ErrorMessage = "{0} int value missing in StatusHistory")]
        public int? PackageRef { get; set; }

        [Required(ErrorMessage = "{0} object missing in StatusHistory")]
        public PackageInformation? Package { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "{0} DateTime value missing in StatusHistory")]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "{0} string value missing in StatusHistory")]
        public string DateOfThisStatus => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

      

    }
}
