using DatabaseServiceContracts;
using ModelsLibrary.DTOs;

namespace DatabaseOperationsTests
{
    public class DatabaseMockOperations
    {
        private readonly IDatabaseService _databaseService;
        public DatabaseMockOperations(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public void AddMockedPackages(List<PackageRequestMock> requestMocks)
        {
            for (int i = 0; i < requestMocks.Count; i++)
            {
                _databaseService.PostPackage(requestMocks[i].GenerateMockObject());
            }

        }
        public async Task<List<PackageInformationResponse>> GetAllPackagesResponse()
        {
            return await _databaseService.GetAllPackagesResponse();
        }

        public async Task<PackageInformationResponse> GetOnePackage(long id)
        {
            PackageInformationResponse? package = await _databaseService.GetOnePackageResponse(id);

            return package;
        }

        public void InsertRandomlyGeneratedMocks(int insertValues)
        {
            List<PackageRequestMock> mockList = MockRequestListGenerator.GenerateMockRequestList(insertValues);

            foreach (PackageRequestMock request in mockList)
            {
                _databaseService.PostPackage(request.GenerateMockObject());
            }

        }

        public void UpdatePackageStatus(List<StatusHistoryRequest> statusHistoryRequestList)
        {
            foreach (StatusHistoryRequest request in statusHistoryRequestList)
            {
                _databaseService.UpdatePackageStatus(request);
            }
        }




    }
}
