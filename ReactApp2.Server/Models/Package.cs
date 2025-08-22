using System.Text.Json.Serialization;



namespace ReactApp2.Server.Models
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

    public class SenderInformation
    {
        public int Id { get; set; }  // Keep this visible

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int PackageRef { get; set; }

        [JsonIgnore]
        public PackageInformation? Package { get; set; }
    }

    public class RecipientInformation
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public int PackageRef { get; set; }

        [JsonIgnore]
        public PackageInformation? Package { get; set; }
    }

    public class StatusHistory
    {
        public int Id { get; set; }
        public string? Status { get; set; }

        public int PackageRef { get; set; }

        [JsonIgnore]
        public PackageInformation? Package { get; set; }

        [JsonIgnore]
        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public string DateOfThisStatus => DisplayDate.ToString("yyyy-MM-dd HH:mm:ss");

    }


}
