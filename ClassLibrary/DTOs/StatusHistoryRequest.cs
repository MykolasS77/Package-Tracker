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
        [Required]
        public string? Status { get; set; }
        [Required]
        public int? PackageRef { get; set; }
    }
}
