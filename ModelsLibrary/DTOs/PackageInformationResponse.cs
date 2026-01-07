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
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} object missing in PackageInformationResponse")]
        public SenderDTO? Sender { get; set; }
        [Required(ErrorMessage = "{0} object missing in PackageInformationResponse")]
        public RecipientDTO? Recipient { get; set; }

        [Required(ErrorMessage = "{0} string value missing in PackageInformationResponse")]
        public string? CurrentStatus { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "{0} DateTime value missing in PackageInformationResponse")]
        public DateTime DisplayDate { get; set; }

        [Required(ErrorMessage = "{0} ICollection value missing in PackageInformationResponse")]
        public ICollection<StatusHistoryResponse> TimeStampHistories { get; set; } = new List<StatusHistoryResponse>();
    }
}
