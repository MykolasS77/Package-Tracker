using DatabaseServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;
using ModelsLibrary.Validation;

namespace PackageTracker.Server.Database
{
    public class DatabaseLogic : IDatabaseService
    {
        private readonly DatabaseContext _context;
        public DatabaseLogic(DatabaseContext context)
        {
            _context = context;
        }

        private void TrySaveChanges(string errorMessage = "Error while trying to save changes after database operation.")
        {
            try
            {
                  _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), errorMessage);
                throw;
            }
        }

        /// <summary>
        /// Gets a list of package data in DTO format, which can later be used for display of all packages, getting single package data, filtering out packages etc.
        /// </summary>
        private IQueryable<PackageInformationResponse> CreatePackageInformationResponseList()
        {

            IQueryable<PackageInformationResponse> packagesResponse = _context.PackageInformations
               .Select(p =>
               new PackageInformationResponse()
               {
                   Id = p.Id,
                   Sender = p.SenderAndRecipientDetails == null ? null : new SenderDTO()
                   {
                       FirstName = p.SenderAndRecipientDetails.SenderFirstName,
                       LastName = p.SenderAndRecipientDetails.SenderLastName,
                       Address = p.SenderAndRecipientDetails.SenderAddress,
                       Phone = p.SenderAndRecipientDetails.SenderPhone,

                   },
                   Recipient = p.SenderAndRecipientDetails == null ? null : new RecipientDTO()
                   {
                       FirstName = p.SenderAndRecipientDetails.RecipientFirstName,
                       LastName = p.SenderAndRecipientDetails.RecipientLastName,
                       Address = p.SenderAndRecipientDetails.RecipientAddress,
                       Phone = p.SenderAndRecipientDetails.RecipientPhone,

                   },
                   CurrentStatus = p.TimeStampHistories
                       .OrderByDescending(h => h.Id)
                       .Select(h => h.Status.ToString())
                       .FirstOrDefault() ?? "Created",
                   TimeStampHistories = p.TimeStampHistories.Select(h => new StatusHistoryResponse
                   {
                       Status = h.Status.ToString(),
                       DisplayDate = h.DisplayDate,
                       DateOfThisStatus = h.DateOfThisStatus,
                   }).ToList()

               });

            return packagesResponse;

        }


        public async Task<List<PackageInformationResponse>> GetAllPackagesResponse()
        {
            return await CreatePackageInformationResponseList().OrderBy(p => p.Id).ToListAsync();
            //OrderBy extension is needed to prevent incorrect order of packages on display when you delete and add a new one.

        }


        public Task<PackageInformationResponse?> GetOnePackageResponse(long id)
        {
            return CreatePackageInformationResponseList().FirstOrDefaultAsync(p => p.Id == id);
        }



        public Task<List<PackageInformationResponse>> FilterPackagesByStatus(string filter)
        {

            ValidationMethods.ValidateStatusFilterValue(filter);

            return CreatePackageInformationResponseList().Where(p => p.CurrentStatus == filter).ToListAsync(); ;

        }


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

        public void DeletePackage(long id)
        {


            PackageInformation? package = _context.PackageInformations.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                throw new Exception($" Package with id {id} not found.");
            }

            _context.PackageInformations.Remove(package);

            TrySaveChanges("Error while deleting a package.");

        }

        public ICollection<StatusHistoryResponse> GetTimestampHistories(long id)
        {

            PackageInformationResponse? package = GetOnePackageResponse(id).Result;

            if (package == null)
            {
                throw new Exception($"Package with id '{id}' not found");
            }


            ICollection<StatusHistoryResponse> packageItemTimestampHistories = package.TimeStampHistories;

            if (packageItemTimestampHistories == null)
            {
                throw new Exception($"TimeStampHistories property not found in package with id {id}.");
            }

            return packageItemTimestampHistories;
        }


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
