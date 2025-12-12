using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Models;


namespace DatabaseServiceContracts
{
    public interface IDatabaseService
    {
        public Task<List<PackageInformation>> GetAllPackages();
        public Task<PackageInformation?> GetOnePackage(long id);

        public Task<List<PackageInformation>> FilterPackages(string filter);

        public void PostPackage(PackageInformation packageItem);
        public void DeletePackage(PackageInformation packageItem);

        public Task<PackageInformation?> UpdateTimeStampInformation(long id);
        public StatusHistory UpdatePackageStatus(StatusHistory newItem);



    }
}
