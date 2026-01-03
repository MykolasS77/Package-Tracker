using DatabaseServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;
using ModelsLibrary.Validation;

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
            ValidationMethods.CheckIfNullOrNegative(id);

            PackageInformationResponse? packageItem = await _databaseService.GetOnePackageResponse(id);

            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

    }

}
