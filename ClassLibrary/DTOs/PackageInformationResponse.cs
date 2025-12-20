using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelsLibrary.DTOs
{
    public class PackageInformationResponse
    {
        public int Id { get; set; }
        [Required]
        public SenderDTO? Sender { get; set; }
        [Required]
        public RecipientDTO? Recipient { get; set; }

        [Required]
        public string? CurrentStatus { get; set; }

        [JsonIgnore]
        public DateTime DisplayDate { get; set; }

        public ICollection<StatusHistoryResponse> TimeStampHistories { get; set; } = new List<StatusHistoryResponse>();
    }
}
