using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    public class PackageInformation
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "{0} object missing in PackageInformation.")]

        public SenderAndRecipientDetails? SenderAndRecipientDetails { get; set; }

        [Required(ErrorMessage = "{0} ICollection value missing in PackageInformation")]
        public ICollection<StatusHistory> TimeStampHistories { get; set; } = new List<StatusHistory>();

    }
}
