using DatabaseServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles 'GET' requests that respond with a list of packages data.
    /// </summary>
    public class GetPackagesController : Controller
    {
        private readonly IDatabaseService _databaseService;

        public GetPackagesController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        [Route("api/getallpackages")]
        public async Task<ActionResult<IEnumerable<PackageInformationResponse>>> DisplayAllPackages()
        {
            List<PackageInformationResponse> packages = await _databaseService.GetAllPackagesResponse();
            return Ok(packages);
        }

        [HttpGet]
        [Route("api/getsinglepackage/{id}")]
        public async Task<ActionResult<PackageInformationResponse>> DisplayPackage(long id)
        {

            PackageInformationResponse? packageItem = await _databaseService.GetOnePackageResponse(id);

            if (packageItem == null || packageItem.TimeStampHistories == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

    }

}
