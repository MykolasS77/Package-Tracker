using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;

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
        private readonly IValidationMethods _validationMethods;

        public DeletePackageController(IDeleteMethods deleteService, IGetMethods getMethods, IValidationMethods validationMethods)
        {
            _deleteService = deleteService;
            _getMethods = getMethods;
            _validationMethods = validationMethods;
        }

        [HttpDelete]
        [Route("api/deletepackage/{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            _validationMethods.CheckIfNull(id);
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