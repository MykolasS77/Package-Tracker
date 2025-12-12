using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;
using DbContextService;
using DatabaseServiceContracts;

namespace PackageTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageInformationController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IDatabaseService _databaseService;

        public PackageInformationController(DatabaseContext context, IDatabaseService databaseService)
        {
            _context = context;
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageInformation>>> GetPackages()
        {
            List<PackageInformation> packages = await _databaseService.GetAllPackages();
            return Ok(packages);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PackageInformation>> GetPackage(long id)
        {
          
            PackageInformation? packageItem = await _databaseService.GetOnePackage(id);

            if (packageItem == null || packageItem.TimeStampHistories == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

        [HttpGet("filterpackages/{filter}")]
        public async Task<ActionResult<PackageInformation>> FilterPackages(string filter)
        {

            List<PackageInformation> packageItem = await _databaseService.FilterPackages(filter);
           

            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);


        }


        [HttpPost]
        public async Task<ActionResult<PackageInformation>> PostPackage(PackageInformation newItem)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid request.");
                
            }

            try
            {
                _databaseService.PostPackage(newItem);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString(), "Error while posting to database");
                return BadRequest();
            }

            return CreatedAtAction("GetPackage", new { id = newItem.Id }, newItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(long id)
        {
            PackageInformation? packageItem = await _databaseService.GetOnePackage(id);
            if (packageItem == null)
            {
                return NotFound();
            }

            _databaseService.DeletePackage(packageItem);

            return Ok();
        }

        [HttpGet("statushistory/{id}")]
        public async Task<ActionResult<PackageInformation>> UpdateTimeStampInformation(long id)
        {
            PackageInformation? packageItem = await _databaseService.UpdateTimeStampInformation(id);

            if (packageItem == null || packageItem.TimeStampHistories == null)
            {
                return NotFound();
            }

            return Ok(packageItem.TimeStampHistories);
        }

        [HttpPost("statushistory")]
        public async Task<ActionResult<StatusHistory>> PostTimeStamp(StatusHistory newItem)
        {
            _databaseService.UpdatePackageStatus(newItem);
         
            return newItem;
        }

    }
}
