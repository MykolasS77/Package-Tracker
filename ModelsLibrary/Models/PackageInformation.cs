using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class PackageInformation
    {
        public int Id { get; set; }  
        [Required]
        public SenderInformation? Sender { get; set; }
        [Required]
        public RecipientInformation? Recipient { get; set; }

        [Required]
        public PackageStatus? CurrentStatus { get; set; } 

        [JsonIgnore]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public string DisplayDateFormatted => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

        public ICollection<StatusHistory> TimeStampHistories { get; set; } = new List<StatusHistory>();

    }
}
