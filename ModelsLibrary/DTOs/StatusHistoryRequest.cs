using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DTOs
{
    public class StatusHistoryRequest
    {
        [Required(ErrorMessage = "{0} string value missing in StatusHistoryRequest")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "{0} int value missing in StatusHistoryRequest")]
        public int? PackageRef { get; set; }
    }
}
