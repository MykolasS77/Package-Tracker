using System.ComponentModel.DataAnnotations;


namespace ModelsLibrary.DTOs
{
    public class StatusHistoryResponse
    {
        [Required(ErrorMessage = "{0} string value missing in StatusHistoryResponse")]
        public string? Status {  get; set; }
        [Required(ErrorMessage = "{0} DateTime value missing in StatusHistoryResponse")]
        public DateTime DisplayDate { get; set; }
        [Required(ErrorMessage = "{0} string value missing in StatusHistoryResponse")]
        public string? DateOfThisStatus { get; set; }


    }
}
