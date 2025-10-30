using System.Text.Json.Serialization;

namespace PackageTracker.Server.Models
{
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
