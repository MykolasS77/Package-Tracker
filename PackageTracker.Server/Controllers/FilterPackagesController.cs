using DatabaseServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;

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
        public async Task<ActionResult<PackageInformationResponse>> FilterPackages(string filter)
        {

            List<PackageInformationResponse> packageItem = await _databaseService.FilterPackages(filter);


            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);


        }
    }

}
