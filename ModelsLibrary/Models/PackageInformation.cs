using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models
{
    public class PackageInformation
    {

        [Key]
        public int Id { get; set; }  
        [Required(ErrorMessage = "{0} object missing in PackageInformation.")] 
        public SenderInformation? Sender { get; set; }
        [Required(ErrorMessage = "{0} object missing in PackageInformation.")]
        public RecipientInformation? Recipient { get; set; }

        [Required(ErrorMessage = "{0} ICollection value missing in PackageInformation")]
        public ICollection<StatusHistory> TimeStampHistories { get; set; } = new List<StatusHistory>();

    }
}
