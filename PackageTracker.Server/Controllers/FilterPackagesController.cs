using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles requests for filtering out packages based on their status. 
    /// </summary>
    public class FilterPackagesController : Controller
    {
        private readonly IGetMethods _getService;
        private readonly IValidationMethods _validationMethods;

        public FilterPackagesController(IGetMethods databaseService, IValidationMethods validationMethods)
        {
            _getService = databaseService;
            _validationMethods = validationMethods;
        }

        [HttpGet]
        [Route("api/filterpackages/{filter}")]
        public async Task<ActionResult<PackageInformationResponse>> FilterPackagesByStatus(string filter)
        {
            _validationMethods.ValidateStatusFilterValue(filter);

            List<PackageInformationResponse> packageItem = await _getService.FilterPackagesByStatus(filter);


            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);


        }
    }

}
