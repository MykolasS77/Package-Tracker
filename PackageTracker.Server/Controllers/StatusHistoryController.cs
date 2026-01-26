using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles requests that return package status history list.
    /// </summary>
    [ApiController]
    public class StatusHistoryController : ControllerBase
    {
        private readonly IUpdateMethods _updateService;
        private readonly IGetMethods _getService;

        public StatusHistoryController(IUpdateMethods updateService, IGetMethods getService)
        {
            _updateService = updateService;
            _getService = getService;
        }

        [HttpGet]
        [Route("api/statushistory/{id}")]
        public async Task<ActionResult<PackageInformation>> GetStatusHistory(int id)
        {

            ICollection<StatusHistoryResponse>? packageItem = _getService.GetStatusHistories(id);


            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

        [HttpPost]
        [Route("/api/statushistory")]
        public async Task<ActionResult<StatusHistory>> AddUpdatedPackageStatus(StatusHistoryRequest? newItem)
        {
            //Status get's updated by adding a new element to the list of StatusHistory. 

            if (!ModelState.IsValid || newItem == null)
            {

                throw new InvalidOperationException("Error with StatusHistoryRequest");
            }

            StatusHistory? updatedItem = _updateService.AddTimestamp(newItem);

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            return updatedItem;
        }

    }
}
