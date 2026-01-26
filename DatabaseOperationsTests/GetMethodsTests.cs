using DatabaseOperationsTests.MockHelpers;
using DatabaseOperationsTests.TestDatabaseOperationsHelpers;
using DbServiceContracts;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;
using PackageTracker.Server.Database.CRUD_Operations;
using Xunit;

namespace DatabaseOperationsTests
{
    public class GetMethodsTests: TestClassBase
    {
        private readonly IGetMethods _getMethods;
        private readonly TestDatabaseOperations _testDatabaseOperations;

        public GetMethodsTests()
        {
            _getMethods = new GetMethods(_inMemoryContext);
            _testDatabaseOperations = new TestDatabaseOperations(_inMemoryContext);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task GetOnePackageResponseTest(int insertMocks)
        {
            _testDatabaseOperations.AutoAddMockedPackages(insertMocks);

            for (int i = 1; i < insertMocks + 1; i++)
            {

                PackageInformationResponse? package = await _getMethods.GetOnePackageResponse(i);

                Assert.NotNull(package);
                Assert.Equal(package.Id, i);

            }

        }

        [Theory]
        [InlineData(nameof(PackageStatus.Canceled), nameof(PackageStatus.Canceled))]
        [InlineData($"{nameof(PackageStatus.Sent)}, {nameof(PackageStatus.Returned)}", nameof(PackageStatus.Returned))]
        public async Task FilterPackagesTest(string statusUpdateSequence, string filterValue)
        {
            int inserMocks = 10;

            _testDatabaseOperations.AutoAddMockedPackages(inserMocks);

            for (int i = 1; i < inserMocks; i += 2)
            {

                List<StatusHistoryRequest> mockList = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList(statusUpdateSequence, i);

                _testDatabaseOperations.ExcecuteUpdateSequenceForOnePackage(mockList);
            }

            List<PackageInformationResponse> filteredPackages = await _getMethods.FilterPackagesByStatus(filterValue);

            foreach (PackageInformationResponse package in filteredPackages)
            {

                Assert.Equal(filterValue, package.CurrentStatus);

            }

        }
    }
}
