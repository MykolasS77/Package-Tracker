using ModelsLibrary.DTOs;
using ModelsLibrary.Models;


namespace DatabaseServiceContracts
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Gets a list of packages ordered by id.
        /// </summary>
        /// <returns>A task with a list of PackageInformatonResponse objects</returns>
        public Task<List<PackageInformationResponse>> GetAllPackagesResponse();
        /// <summary>
        /// Gets one package.
        /// </summary>
        /// <returns>A task with PackageInformationResponse object</returns>
        public Task<PackageInformationResponse?> GetOnePackageResponse(long id);
        /// <summary>
        /// Gets a filtered packages list based on specified status.
        /// </summary>
        /// <param name="filter">A string value that should match one of the arguments from the enum list in PackageStatus.cs</param>
        public Task<List<PackageInformationResponse>> FilterPackagesByStatus(string filter);
        /// <summary>
        /// Adds a new package to a database.
        /// </summary>
        /// <param name="packageItem">Details on new package provided by the client.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void PostPackage(PackageInformationRequest packageItem);

        /// <summary>
        /// Deletes one package from the database.
        /// </summary>
        /// <param name="id">PackageId parameter</param>
        public void DeletePackage(long id);

        /// <summary>
        /// Gets timestamp history data which is used for displaying package status history table.
        /// </summary>
        /// <returns>A collection of StatusHistoryResponse objects</returns>
        public ICollection<StatusHistoryResponse> GetTimestampHistories(long id);

        /// <summary>
        /// Updates current package status by adding a new status to history table.
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public StatusHistory UpdatePackageStatus(StatusHistoryRequest newItem);

    }
}
