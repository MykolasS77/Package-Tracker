using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;
using DatabaseServiceContracts;
using ModelsLibrary.DTOs;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles 'POST' requests for adding a new PackageInformation type item to database.
    /// </summary>
    [ApiController]
    public class CreatePackageController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public CreatePackageController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        [Route("api/createpackage")]
        public async Task<ActionResult<PackageInformation>> CreateNewPackage(PackageInformationRequest newItem)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid request.");

            }

            _databaseService.PostPackage(newItem);

            return CreatedAtAction(nameof(CreateNewPackage), new { id = newItem.Id }, newItem);

        }

    }
}
