using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class PackageInformation
    {
        
        public int Id { get; set; }  
        [Required(ErrorMessage = "{0} object missing in PackageInformation.")] 
        public SenderInformation? Sender { get; set; }
        [Required(ErrorMessage = "{0} object missing in PackageInformation.")]
        public RecipientInformation? Recipient { get; set; }

        [JsonIgnore]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public string DisplayDateFormatted => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

        [Required(ErrorMessage = "{0} ICollection value missing in PackageInformation")]
        public ICollection<StatusHistory> TimeStampHistories { get; set; } = new List<StatusHistory>();

    }
}
