using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class SenderAndRecipientDetails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? SenderFirstName { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? SenderLastName { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? SenderAddress { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? SenderPhone { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? RecipientFirstName { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? RecipientLastName { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? RecipientAddress { get; set; }
        [Required(ErrorMessage = "{0} string missing in PackageDetails")]
        public string? RecipientPhone { get; set; }
        public int PackageRef { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "{0} object missing in RecipientInformation")]
        public PackageInformation? Package { get; set; }
    }
    
}
