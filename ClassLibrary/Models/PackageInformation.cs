using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Models
{
    public class PackageInformation
    {
        public int Id { get; set; }  
        [Required]
        public SenderInformation Sender { get; set; } = null!;
        [Required]
        public RecipientInformation Recipient { get; set; } = null!;

        [Required]
        public string CurrentStatus { get; set; } = null!; 

        [JsonIgnore]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public string DisplayDateFormatted => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

        public ICollection<StatusHistory> TimeStampHistories { get; } = new List<StatusHistory>();

    }
}
