using DbServiceContracts;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Database.CRUD_Operations
{
    public class UpdatePackageStatusMethods : DatabaseLogic, IUpdateMethods
    {
        private readonly IValidationMethods _validationMethods;
        public UpdatePackageStatusMethods(DatabaseContext context, IValidationMethods validationMethods) : base(context) { 
        
            _validationMethods = validationMethods; 

        }
        public StatusHistory AddTimestamp(StatusHistoryRequest request)
        {

            PackageStatus newStatus = TryStringToEnumConvert(request.Status, (int)request.PackageRef);

            StatusHistory newPackage = new StatusHistory()
            {
                Status = newStatus,
                PackageRef = request.PackageRef,

            };

            _context.StatusHistories.Add(newPackage);

            TrySaveChanges("Error while adding a package to database.");

            return newPackage;



        }

        private PackageStatus TryStringToEnumConvert(string value, int packageId)
        {
            _validationMethods.ValidateStatusFilterValue(value);
            _validationMethods.CheckUpdateAvailability(value, packageId);

            return (PackageStatus)Enum.Parse(typeof(PackageStatus), value);

        }
    }
}
