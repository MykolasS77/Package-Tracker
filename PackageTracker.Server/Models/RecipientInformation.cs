using System.Text.Json.Serialization;

namespace PackageTracker.Server.Models
{
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
}
