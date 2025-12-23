using ModelsLibrary.DTOs;
using ModelsLibrary.Models;


namespace DatabaseServiceContracts
{
    public interface IDatabaseService
    {
        public Task<List<PackageInformationResponse>> GetAllPackagesResponse();
        public Task<PackageInformationResponse?> GetOnePackageResponse(long id);

        public Task<List<PackageInformationResponse>> FilterPackages(string filter);

        public void PostPackage(PackageInformationRequest packageItem);
        public void DeletePackage(long id);

        public ICollection<StatusHistoryResponse> GetTimestampHistories(long id);
        public StatusHistory? AddNewStatusToHistoryTable(StatusHistoryRequest newItem);

    }
}
