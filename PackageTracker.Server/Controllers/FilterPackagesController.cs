using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;
using ModelsLibrary.Validation;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles requests for filtering out packages based on their status. 
    /// </summary>
    public class FilterPackagesController : Controller
    {
        private readonly IGetMethods _getService;

        public FilterPackagesController(IGetMethods databaseService)
        {
            _getService = databaseService;
        }

        [HttpGet]
        [Route("api/filterpackages/{filter}")]
        public async Task<ActionResult<PackageInformationResponse>> FilterPackagesByStatus(string filter)
        {
            ValidationMethods.ValidateStatusFilterValue(filter);

            List<PackageInformationResponse> packageItem = await _getService.FilterPackagesByStatus(filter);


            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);


        }
    }

}
