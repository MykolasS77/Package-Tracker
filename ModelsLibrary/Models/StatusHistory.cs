using ModelsLibrary.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class StatusHistory
    {
        public int Id { get; set; }
        [Required]
        public PackageStatus? Status { get; set; }

        [Required]
        public int? PackageRef { get; set; }

        [JsonIgnore]
        public PackageInformation? Package { get; set; }

        [JsonIgnore]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public string DateOfThisStatus => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

      

    }
}
