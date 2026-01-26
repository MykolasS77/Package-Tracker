using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;
using PackageTracker.Server.Database.Validation;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles 'GET' requests that respond with a list of packages data.
    /// </summary>
    public class GetPackagesController : Controller
    {
        private readonly IGetMethods _getService;
        private readonly IValidationMethods _validationMethods;

        public GetPackagesController(IGetMethods getService, IValidationMethods validationMethods)
        {
            _getService = getService;
            _validationMethods = validationMethods;
        }

        [HttpGet]
        [Route("api/getallpackages")]
        public async Task<ActionResult<IEnumerable<PackageInformationResponse>>> DisplayAllPackages()
        {
            List<PackageInformationResponse> packages = await _getService.GetAllPackagesResponse();
            return Ok(packages);
        }

        [HttpGet]
        [Route("api/getsinglepackage/{id}")]
        public async Task<ActionResult<PackageInformationResponse>> DisplayPackage(int id)
        {
            _validationMethods.CheckIfNull(id);

            PackageInformationResponse? packageItem = await _getService.GetOnePackageResponse(id);

            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

    }

}
