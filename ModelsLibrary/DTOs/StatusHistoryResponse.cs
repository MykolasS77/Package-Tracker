using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DTOs
{
    public class StatusHistoryResponse
    {
        [Required]
        public string? Status {  get; set; }
        [Required]
        public DateTime DisplayDate { get; set; }
        [Required]
        public string? DateOfThisStatus { get; set; }


    }
}
