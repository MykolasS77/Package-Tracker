using DbServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Controllers
{
    /// <summary>
    /// Handles 'POST' requests for adding a new PackageInformation type item to database.
    /// </summary>
    [ApiController]
    public class CreatePackageController : ControllerBase
    {
        private readonly IPostMethods _postMethods;

        public CreatePackageController(IPostMethods databaseService)
        {
            _postMethods = databaseService;
        }

        [HttpPost]
        [Route("api/createpackage")]
        public async Task<ActionResult<PackageInformation>> CreateNewPackage(PackageInformationRequest newItem)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid request.");

            }

            _postMethods.PostPackage(newItem);

            return CreatedAtAction(nameof(CreateNewPackage), new { id = newItem.Id }, newItem);

        }

    }
}
