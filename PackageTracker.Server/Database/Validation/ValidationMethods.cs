using DbServiceContracts;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;
using PackageTracker.Server.Database.CRUD_Operations;
using System.Linq.Expressions;

namespace PackageTracker.Server.Database.Validation
{
    public class ValidationMethods: GetMethods, IValidationMethods
    {
        public ValidationMethods(DatabaseContext context) : base(context) { }
       

        public void CheckIfNull(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Id cannot be a 0");
            }
        }

        public void ValidateStatusFilterValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("String value for enum conversion not provided.");
            }

            bool validValue = CheckIfFilterValueExists(value);

            if (!validValue)
            {
                throw new ArgumentException($"Value: {value} did not match any cases.");
            }


        }

        private bool CheckIfFilterValueExists(string value)
        {

            foreach (int status in Enum.GetValues(typeof(PackageStatus)))
            {
                string? packageStatusValue = Enum.GetName(typeof(PackageStatus), status);

                if (packageStatusValue != null && packageStatusValue == value)
                {
                    return true;
                }
            }
            return false;

        }

        //Status updates explanation:
        //      • Created → Can be changed to: Sent, Canceled     
        //      • Sent → Can be changed to: Accepted, Returned, Canceled
        //      • Returned → Can be changed to: Sent, Canceled
        //      • Accepted → Final status, cannot be changed
        //      • Canceled → Final status, cannot be changed

        public void CheckUpdateAvailability(string updateStringRepresentation, int packageId)
        {

            if (string.IsNullOrEmpty(updateStringRepresentation))
            {
                throw new Exception("Status update value was not provided");
            }

            if (packageId <= 0)
            {
                throw new Exception("PackageId must be a positive number.");
            }

            if (updateStringRepresentation == nameof(PackageStatus.Created))
            {
                throw new Exception("Can't update package status to 'Created'");
            }

           



            string? currentStatus = GetStatusHistories(packageId).Last().Status;

            if (currentStatus == null) {
                throw new Exception("'Status' variable not found in StatusHistories");
            }

            if (currentStatus == updateStringRepresentation)
            {
                throw new Exception("Status update value matches current package status value.");
            }

            if (currentStatus == nameof(PackageStatus.Accepted) || currentStatus == nameof(PackageStatus.Canceled))
            {
                throw new Exception($"Can't update package status if current status is set to {nameof(PackageStatus.Accepted)}  or {nameof(PackageStatus.Canceled)}");
            }


            if (currentStatus == nameof(PackageStatus.Created) && updateStringRepresentation != nameof(PackageStatus.Sent) && updateStringRepresentation != nameof(PackageStatus.Canceled))
            {
                throw new Exception($"Can't change from '{PackageStatus.Created}' to '{updateStringRepresentation}. The update value in this case must be either '{nameof(PackageStatus.Sent)}' or '{nameof(PackageStatus.Canceled)}'");
            }

            if (currentStatus == nameof(PackageStatus.Returned) && updateStringRepresentation != nameof(PackageStatus.Canceled))
            {
                throw new Exception($"Can't change from '{PackageStatus.Returned}' to '{updateStringRepresentation}. The update value in this case must be '{nameof(PackageStatus.Canceled)}'");
            }




        }




        
    }
}
