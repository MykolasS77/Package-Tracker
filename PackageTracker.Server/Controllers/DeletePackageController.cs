using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;
using ModelsLibrary.Validation;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles request for deleting packages from database.
    /// </summary>
    [ApiController]
    public class DeletePackageController : ControllerBase
    {
        private readonly IDeleteMethods _deleteService;
        private readonly IGetMethods _getMethods;

        public DeletePackageController(IDeleteMethods deleteService, IGetMethods getMethods)
        {
            _deleteService = deleteService;
            _getMethods = getMethods;
        }

        [HttpDelete]
        [Route("api/deletepackage/{id}")]
        public async Task<IActionResult> DeletePackage(long id)
        {
            ValidationMethods.CheckIfNullOrNegative(id);
            PackageInformationResponse? packageItem = await _getMethods.GetOnePackageResponse(id);
            if (packageItem == null)
            {
                return NotFound();
            }

            _deleteService.DeletePackage(id);

            return Ok();
        }
    }
}