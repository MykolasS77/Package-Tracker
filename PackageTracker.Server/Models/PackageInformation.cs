using System.Text.Json.Serialization;

namespace PackageTracker.Server.Models
{
    public class PackageInformation
    {
        public int Id { get; set; }  // Keep this visible so client knows the new package Id
        public SenderInformation Sender { get; set; } = null!;

        public RecipientInformation Recipient { get; set; } = null!;

        public string CurrentStatus { get; set; } = null!;

        [JsonIgnore]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public string DisplayDateFormatted => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

        public ICollection<StatusHistory> TimeStampHistories { get; } = new List<StatusHistory>();

    }
}
