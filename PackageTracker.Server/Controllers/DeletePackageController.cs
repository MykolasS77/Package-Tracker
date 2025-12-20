using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using DatabaseServiceContracts;
using ModelsLibrary.DTOs;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles request for deleting packages from database.
    /// </summary>
    [ApiController]
    public class DeletePackageController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public DeletePackageController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        [Route("api/deletepackage/{id}")]
        public async Task<IActionResult> DeletePackage(long id)
        {
            PackageInformationResponse? packageItem = await _databaseService.GetOnePackageResponse(id);
            if (packageItem == null)
            {
                return NotFound();
            }

            _databaseService.DeletePackage(id);

            return Ok();
        }
    }
}