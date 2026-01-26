using DatabaseOperationsTests.HelpersForTesting;
using DatabaseOperationsTests.MockHelpers;
using DatabaseOperationsTests.TestDatabaseOperationsHelpers;
using DbServiceContracts;
using ModelsLibrary.DTOs;
using PackageTracker.Server.Database.CRUD_Operations;
using Xunit;

namespace DatabaseOperationsTests
{
    public class PostMethodsTests: TestClassBase
    {

        private readonly IGetMethods _getMethods;
        private readonly TestDatabaseOperations _testDatabaseOperations;

        public PostMethodsTests()
        {
            _getMethods = new GetMethods(_inMemoryContext);
            _testDatabaseOperations = new TestDatabaseOperations(_inMemoryContext);
        }

        [Fact]
        public async Task PostSinglePackage_ManuallyAddedValues()
        {

            List<PackageRequestMock> requestMocks = new List<PackageRequestMock>() {

                new PackageRequestMock(
                    recipientFirstName: "Donald",
                    recipientLastName: "Duck",
                    recipientPhone: "123456789",
                    recipientAdress: "Duckburg",
                    senderFirstName: "Mickey",
                    senderLastName: "Mouse",
                    senderAdress: "Toontown",
                    senderPhone: "987654321"),
                new PackageRequestMock(
                    recipientFirstName: "Jon",
                    recipientLastName: "Snow",
                    recipientPhone: "123456789",
                    recipientAdress: "The Wall",
                    senderFirstName: "Ned",
                    senderLastName: "Stark",
                    senderAdress: "The North",
                    senderPhone: "987654321"
                    )

            };

            _testDatabaseOperations.AddPredefinedMocks(requestMocks);

            List<PackageInformationResponse> packagesResponse = await _getMethods.GetAllPackagesResponse();

            TestHelpers.CompareResponseAgainstMock(packagesResponse, requestMocks);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task PostSeveralPackages_RandomlyGeneratedValues(int insertValues)
        {
            List<PackageRequestMock> requestMocks = MockRequestListGenerator.GenerateMockRequestList(insertValues);

            _testDatabaseOperations.AddPredefinedMocks(requestMocks);

            List<PackageInformationResponse> packagesResponses = await _getMethods.GetAllPackagesResponse();

            Assert.NotEmpty(packagesResponses);
            TestHelpers.CheckIfInitialStatusesSetToCreated(packagesResponses);
            TestHelpers.CompareResponseAgainstMock(packagesResponses, requestMocks);
            TestHelpers.CheckIfKeyValuesAscending(packagesResponses);

        }
    }
}
