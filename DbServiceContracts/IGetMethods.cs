using ModelsLibrary.DTOs;

namespace DbServiceContracts
{
    public interface IGetMethods
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
        public Task<PackageInformationResponse?> GetOnePackageResponse(int id);
        /// <summary>
        /// Gets a filtered packages list based on specified status.
        /// </summary>
        /// <param name="filter">A string value that should match one of the arguments from the enum list in PackageStatus.cs</param>
        public Task<List<PackageInformationResponse>> FilterPackagesByStatus(string filter);

        /// <summary>
        /// Gets timestamp history data which is used for displaying package status history table.
        /// </summary>
        /// <returns>A collection of StatusHistoryResponse objects</returns>
        public List<StatusHistoryResponse> GetStatusHistories(int id);
    }
}
