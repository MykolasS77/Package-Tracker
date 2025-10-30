using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackageTracker.Server.Models;
using TodoApi.Models;

namespace PackageTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageInformationController : ControllerBase
    {
        private readonly PackageInformationContext _context;

        public PackageInformationController(PackageInformationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageInformation>>> GetPackages()
        {
            return await _context.PackageInformations
                .Include(p => p.Sender)
                .Include(p => p.Recipient)
                .Include(p => p.TimeStampHistories)
                .ToListAsync();
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<PackageInformation>> GetPackage(long id)
        {
            var packageItem = await _context.PackageInformations
                .Include(p => p.Sender)
                .Include(p => p.Recipient)
                .Include(p => p.TimeStampHistories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (packageItem == null || packageItem.TimeStampHistories == null)
            {
                return NotFound();
            }

            return Ok(packageItem);
        }

        [HttpGet("filterpackages/{filter}")]
        public async Task<ActionResult<PackageInformation>> FilterPackages(string filter)
        {

            var packageItem = await _context.PackageInformations
            .Include(p => p.Sender)
            .Include(p => p.Recipient)
            .Include(p => p.TimeStampHistories)
            .Where(p => p.CurrentStatus == filter)
            .ToListAsync();

            if (packageItem == null)
            {
                return NotFound();
            }

            return Ok(packageItem);


        }





        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(long id, PackageInformation packageItem)
        {
            if (id != packageItem.Id)
            {
                return BadRequest();
            }



            _context.Entry(packageItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<PackageInformation>> PostPackage(PackageInformation packageItem)
        {
            _context.PackageInformations.Add(packageItem);

            await PostTimeStamp(packageItem.TimeStampHistories.FirstOrDefault() ?? new StatusHistory
            {
                PackageRef = packageItem.Id,
                Status = packageItem.CurrentStatus
            });

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = packageItem.Id }, packageItem);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(long id)
        {
            var packageItem = await _context.PackageInformations.FindAsync(id);
            if (packageItem == null)
            {
                return NotFound();
            }

            _context.PackageInformations.Remove(packageItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("statushistory/{id}")]
        public async Task<ActionResult<PackageInformation>> GetTimeStampInformation(long id)
        {
            var packageItem = await _context.PackageInformations

                .Include(p => p.TimeStampHistories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (packageItem == null || packageItem.TimeStampHistories == null)
            {
                return NotFound();
            }

            return Ok(packageItem.TimeStampHistories);
        }

        [HttpPost("statushistory")]
        public async Task<ActionResult<StatusHistory>> PostTimeStamp(StatusHistory newItem)
        {
            _context.StatusHistories.Add(newItem);
            var package = await _context.PackageInformations.FirstOrDefaultAsync(u => u.Id == newItem.PackageRef);

            if (package != null)
            {
                package.CurrentStatus = newItem.Status;
            }

            await _context.SaveChangesAsync();

            return newItem;
        }


        private bool PackageExists(long id)
        {
            return _context.PackageInformations.Any(e => e.Id == id);
        }
    }
}
