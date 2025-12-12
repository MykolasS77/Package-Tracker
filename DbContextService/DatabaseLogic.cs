using DatabaseServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextService
{
    
    public class DatabaseLogic : IDatabaseService
    {
        private readonly DatabaseContext _context;

        public DatabaseLogic(DatabaseContext context)
        {
            _context = context; 
        }
        public Task<List<PackageInformation>> GetAllPackages() {

            return _context.PackageInformations
               .Include(p => p.Sender)
               .Include(p => p.Recipient)
               .Include(p => p.TimeStampHistories)
               .ToListAsync();

        }

        public Task<PackageInformation?> GetOnePackage(long id)
        {

            Task<PackageInformation?> packageItem = _context.PackageInformations
                .Include(p => p.Sender)
                .Include(p => p.Recipient)
                .Include(p => p.TimeStampHistories)
                .FirstOrDefaultAsync(p => p.Id == id);

            return packageItem;

        }

        public Task<List<PackageInformation>> FilterPackages(string filter)
        {
            Task<List<PackageInformation>> packageItem = _context.PackageInformations
            .Include(p => p.Sender)
            .Include(p => p.Recipient)
            .Include(p => p.TimeStampHistories)
            .Where(p => p.CurrentStatus == filter)
            .ToListAsync();

            return packageItem;

        }

        public void PostPackage(PackageInformation packageItem)
        {
            _context.PackageInformations.Add(packageItem);

            UpdatePackageStatus(packageItem.TimeStampHistories.FirstOrDefault() ?? new StatusHistory
            {
                PackageRef = packageItem.Id,
                Status = packageItem.CurrentStatus
            });

            _context.SaveChangesAsync();

        }

        public void DeletePackage(PackageInformation packageItem)
        {
            _context.PackageInformations.Remove(packageItem);
            _context.SaveChangesAsync();
        }

        public Task<PackageInformation?> UpdateTimeStampInformation(long id)
        {
            var packageItem = _context.PackageInformations
                .Include(p => p.TimeStampHistories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (packageItem == null || packageItem.Result.TimeStampHistories == null)
            {
                return null;
            }

            return packageItem;
        }
        public StatusHistory UpdatePackageStatus(StatusHistory newItem)
        {
            _context.StatusHistories.Add(newItem);

            Task<PackageInformation?> package = _context.PackageInformations.FirstOrDefaultAsync(u => u.Id == newItem.PackageRef);
            
            if (package.Result != null)
            {

                package.Result.CurrentStatus = newItem.Status;
            }

             _context.SaveChangesAsync();

            return newItem;
        }

        


    }
}
