using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DTOs
{
    public class PackageInformationRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} object missing in PackageInformationRequest")]
        public SenderDTO? Sender { get; set; }

        [Required(ErrorMessage = "{0} object missing in PackageInformationRequest")]
        public RecipientDTO? Recipient { get; set; }

        [Required(ErrorMessage = "{0} string value missing in PackageInformationRequest")]
        public string? CurrentStatus { get; set; }

        [Required(ErrorMessage = "{0} ICollection value missing in PackageInformationRequest")]
        public ICollection<StatusHistory> TimeStampHistories { get; set; } = new List<StatusHistory>();
    }
}
