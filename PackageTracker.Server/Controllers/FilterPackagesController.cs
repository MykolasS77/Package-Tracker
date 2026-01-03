using DatabaseServiceContracts;
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
        private readonly IDatabaseService _databaseService;

        public FilterPackagesController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        
        [HttpGet]
        [Route("api/filterpackages/{filter}")]
        public async Task<ActionResult<PackageInformationResponse>> FilterPackagesByStatus(string filter)
        {
            ValidationMethods.ValidateStatusFilterValue(filter);

            List<PackageInformationResponse> packageItem = await _databaseService.FilterPackagesByStatus(filter);


            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);


        }
    }

}
