using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using DatabaseServiceContracts;
using ModelsLibrary.DTOs;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles requests that return package status history list.
    /// </summary>
    [ApiController]
    public class StatusHistoryController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public StatusHistoryController( IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        [Route("api/statushistory/{id}")]
        public async Task<ActionResult<PackageInformation>> GetStatusHistory(long id)
        {

            ICollection<StatusHistoryResponse>? packageItem = _databaseService.GetTimestampHistories(id);


            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

        [HttpPost]
        [Route("/api/statushistory")]
        public async Task<ActionResult<StatusHistory>> UpdatePacakgeStatus(StatusHistoryRequest? newItem)
        {

            if (!ModelState.IsValid || newItem == null) {

                throw new InvalidOperationException("Error with StatusHistoryRequest");
            }

            StatusHistory? updatedItem = _databaseService.AddNewStatusToHistoryTable(newItem);

            if (updatedItem == null) { 
                throw new ArgumentNullException(nameof(newItem));
            }
         
            return updatedItem;
        }

    }
}
