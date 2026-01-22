using DbServiceContracts;
using ModelsLibrary.DTOs;

namespace DatabaseOperationsTests
{
    public class DatabaseMockOperations
    {
        private readonly IGetMethods _getMethods;
        private readonly IPostMethods _postMethods;
        private readonly IDeleteMethods _deleteMethods;
        private readonly IUpdateMethods _updateMethods;
        public DatabaseMockOperations(IGetMethods getMethods, IPostMethods postMethods, IDeleteMethods deleteMethods, IUpdateMethods updateMethods)
        {
            _getMethods = getMethods;
            _postMethods = postMethods;
            _deleteMethods = deleteMethods;
            _updateMethods = updateMethods;
        }
        public async Task<List<PackageInformationResponse>> GetAllPackagesResponse()
        {
            return await _getMethods.GetAllPackagesResponse();
        }

        public async Task<PackageInformationResponse> GetOnePackage(long id)
        {
            PackageInformationResponse? package = await _getMethods.GetOnePackageResponse(id);

            return package;
        }
        public void AddMockedPackages(List<PackageRequestMock> requestMocks)
        {
            for (int i = 0; i < requestMocks.Count; i++)
            {
                _postMethods.PostPackage(requestMocks[i].GenerateMockObject());
            }

        }

        public void AddSingleMockedPackage(PackageRequestMock requestMock)
        {
            _postMethods.PostPackage(requestMock.GenerateMockObject());

        }

        public void InsertRandomlyGeneratedMocks(int insertValues)
        {
            List<PackageRequestMock> mockList = MockRequestListGenerator.GenerateMockRequestList(insertValues);

            foreach (PackageRequestMock request in mockList)
            {
                _postMethods.PostPackage(request.GenerateMockObject());
            }

        }

        public void UpdatePackageStatus(List<StatusHistoryRequest> statusHistoryRequestList)
        {
            foreach (StatusHistoryRequest request in statusHistoryRequestList)
            {
                _updateMethods.UpdatePackageStatus(request);
            }
        }




    }
}
