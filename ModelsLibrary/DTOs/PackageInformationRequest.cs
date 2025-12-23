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
        
        public SenderDTO? Sender { get; set; }
        
        public RecipientDTO? Recipient { get; set; }

        
        public string? CurrentStatus { get; set; }

        public ICollection<StatusHistory> TimeStampHistories { get; set; } = new List<StatusHistory>();
    }
}
