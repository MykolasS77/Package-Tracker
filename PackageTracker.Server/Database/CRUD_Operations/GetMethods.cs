using DbServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DTOs;
using ModelsLibrary.Validation;

namespace PackageTracker.Server.Database.CRUD_Operations
{
    public class GetMethods : DatabaseLogic, IGetMethods
    {
        public GetMethods(DatabaseContext context) : base(context) { }

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
    }
}
