using DbServiceContracts;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Database.CRUD_Operations
{
    public class PostNewPackageMethods : DatabaseLogic, IPostMethods
    {
        public PostNewPackageMethods(DatabaseContext context) : base(context) { }
        public void PostPackage(PackageInformationRequest packageItem)
        {

            PackageInformation newItem = new PackageInformation()
            {
                SenderAndRecipientDetails = new SenderAndRecipientDetails()
                {
                    SenderFirstName = packageItem?.Sender?.FirstName,
                    SenderLastName = packageItem?.Sender?.LastName,
                    SenderAddress = packageItem?.Sender?.Address,
                    SenderPhone = packageItem?.Sender?.Phone,
                    RecipientFirstName = packageItem?.Recipient?.FirstName,
                    RecipientLastName = packageItem?.Recipient?.LastName,
                    RecipientAddress = packageItem?.Recipient?.Address,
                    RecipientPhone = packageItem?.Recipient?.Phone,

                }

            };

            _context.PackageInformations.Add(newItem);

            TrySaveChanges("Error while adding PackageInformation table to database.");


            _context.StatusHistories.Add(new StatusHistory
            {
                PackageRef = newItem.Id,
                Status = PackageStatus.Created
            });

            TrySaveChanges("Error while adding StatusHistory table to database.");


        }
    }
}
