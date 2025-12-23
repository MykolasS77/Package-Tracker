using DatabaseServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;



namespace DbContextService
{
    public class DatabaseLogic : IDatabaseService
    {
        private readonly DatabaseContext _context;

        public DatabaseLogic(DatabaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets a list of package data in DTO format, which can later be used for display of all packages, getting single package data, filtering out packages etc.
        /// </summary>
        private IQueryable<PackageInformationResponse> CreatePackageInformationResponseList()
        {
            
            return _context.PackageInformations
               .Select(p =>             
               new PackageInformationResponse()
               {
                   Id = p.Id,
                   Sender = p.Sender == null ? null : new SenderDTO()
                   {
                       FirstName = p.Sender.FirstName,
                       LastName = p.Sender.LastName,
                       Address = p.Sender.Address,
                       Phone = p.Sender.Phone,

                   },
                   Recipient = p.Recipient == null ? null : new RecipientDTO()
                   {
                       FirstName = p.Recipient.FirstName,
                       LastName = p.Recipient.LastName,
                       Address = p.Recipient.Address,
                       Phone = p.Recipient.Phone,

                   },
                   CurrentStatus = p.TimeStampHistories.Last().Status.ToString(),
                   TimeStampHistories = p.TimeStampHistories.Select(h => new StatusHistoryResponse
                   {
                       Status = h.Status.ToString(),
                       DisplayDate = h.DisplayDate,
                       DateOfThisStatus = h.DateOfThisStatus,
                   }).ToList()
                   
               });

        }

       
        public Task<List<PackageInformationResponse>> GetAllPackagesResponse() {

            return CreatePackageInformationResponseList().OrderBy(p => p.Id).ToListAsync();

            //OrderBy extension is needed to prevent incorrect order of packages on display when you delete and add a new one.

        }


        public Task<PackageInformationResponse?> GetOnePackageResponse(long id)
        {
            return CreatePackageInformationResponseList().FirstOrDefaultAsync(p => p.Id == id); ;

        }


        
        public Task<List<PackageInformationResponse>> FilterPackages(string filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return CreatePackageInformationResponseList().Where(p => p.CurrentStatus == filter).ToListAsync(); ;

        }

        
        public void PostPackage(PackageInformationRequest packageItem)
        {
            if(packageItem == null)
            {
                throw new ArgumentNullException("Error while creating a new package. PackageItem argument not provided.");
            }

            if (packageItem.Sender == null || packageItem.Recipient == null)
            {
                throw new ArgumentNullException("Error while creating a new package. Sender or Recipient object not found PackageInformationRequest type argument.");
            }
            if (packageItem.CurrentStatus == null) {
                throw new ArgumentNullException("Error while creating a new package. Object argument 'CurrentStatus' not provided.");
            }

            PackageInformation newItem = new PackageInformation()
            {
                Sender = new SenderInformation()
                {
                    FirstName = packageItem.Sender.FirstName,
                    LastName = packageItem.Sender.LastName,
                    Address = packageItem.Sender.Address,
                    Phone = packageItem.Sender.Phone,
                },
                Recipient = new RecipientInformation()
                {
                    FirstName = packageItem.Recipient.FirstName,
                    LastName = packageItem.Recipient.LastName,
                    Address = packageItem.Recipient.Address,
                    Phone = packageItem.Recipient.Phone,

                },
                
            };

            try
            {
                _context.PackageInformations.Add(newItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), "Error while addng PackageInformation istance.");
            }

            if (newItem.TimeStampHistories.FirstOrDefault() == null)
            {
                try {
                    _context.StatusHistories.Add(new StatusHistory
                    {
                        PackageRef = newItem.Id,
                        Status = PackageStatus.Created
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString(), "Error while addng StatusHistory instance.");
                }


            }

            _context.SaveChangesAsync();

        }

        public void DeletePackage(long id)
        {
            PackageInformation? package = _context.PackageInformations.FirstOrDefault(x => x.Id == id);
            if (package == null) {
                throw new Exception($"Error while trying to delete a package. Package with id {id} not found.");
            }
            _context.PackageInformations.Remove(package);
            _context.SaveChangesAsync();
        }

        public ICollection<StatusHistoryResponse> GetTimestampHistories(long id)
        {

            PackageInformationResponse? package = GetOnePackageResponse(id).Result;

            if (package == null) {
                throw new Exception($"Package with id {id} not found");
            }

            ICollection <StatusHistoryResponse> packageItemTimestampHistories = package.TimeStampHistories;
          
            if (packageItemTimestampHistories == null)
            {
                throw new Exception($"TimeStampHistories property not found in package with id {id}.");
            }

            return packageItemTimestampHistories;
        }

        
        public StatusHistory UpdatePackageStatus(StatusHistoryRequest? newItem)
        {
            
            if (newItem == null || newItem.Status == null || newItem.PackageRef == null) {
                throw new ArgumentNullException(nameof(newItem));
            }

            StatusHistory newPackage = new StatusHistory()
            {
                Status = PackageStatusMethods.StringToEnumConvert(newItem.Status),
                PackageRef = newItem.PackageRef,

            };

            _context.StatusHistories.Add(newPackage);

            _context.SaveChangesAsync();

            return newPackage;
        }

    }
}
