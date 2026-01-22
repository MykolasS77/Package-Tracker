using DbServiceContracts;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Database.CRUD_Operations
{
    public class UpdatePackageStatusMethods : DatabaseLogic, IUpdateMethods
    {
        public UpdatePackageStatusMethods(DatabaseContext context) : base(context) { }
        public StatusHistory UpdatePackageStatus(StatusHistoryRequest? request)
        {

            StatusHistory newPackage = new StatusHistory()
            {
                Status = PackageStatusMethods.TryStringToEnumConvert(request.Status),
                PackageRef = request.PackageRef,

            };

            _context.StatusHistories.Add(newPackage);

            TrySaveChanges("Error while adding a package to database.");

            return newPackage;
        }
    }
}
