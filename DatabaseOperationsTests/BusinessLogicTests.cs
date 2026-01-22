using DbServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DTOs;
using PackageTracker.Server.Database;
using PackageTracker.Server.Database.CRUD_Operations;
using Xunit;

namespace DatabaseOperationsTests
{
    public class BusinessLogicTests
    {
        private readonly IGetMethods _getMethods;
        private readonly IPostMethods _postMethods;
        private readonly IDeleteMethods _deleteMethods;
        private readonly IUpdateMethods _updateMethods;

        private DatabaseMockOperations _databaseMockOperations;
        public BusinessLogicTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            DatabaseContext context = new DatabaseContext(options);
            _getMethods = new GetMethods(context);
            _postMethods = new PostNewPackageMethods(context);
            _deleteMethods = new DeletePackageMethods(context);
            _postMethods = new PostNewPackageMethods(context);
            _updateMethods = new UpdatePackageStatusMethods(context);

            _databaseMockOperations = new DatabaseMockOperations(_getMethods, _postMethods, _deleteMethods, _updateMethods);

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

            _databaseMockOperations.AddMockedPackages(requestMocks);

            List<PackageInformationResponse> packagesResponse = await _databaseMockOperations.GetAllPackagesResponse();

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

            _databaseMockOperations.AddMockedPackages(requestMocks);
            List<PackageInformationResponse> packagesResponses = await _databaseMockOperations.GetAllPackagesResponse();

            Assert.NotEmpty(packagesResponses);
            TestHelpers.CheckIfInitialStatusesSetToCreated(packagesResponses);
            TestHelpers.CompareResponseAgainstMock(packagesResponses, requestMocks);
            TestHelpers.CheckIfKeyValuesAscending(packagesResponses);

        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]

        public async Task GetOnePackageResponseTest(int insertValues)
        {
            _databaseMockOperations.InsertRandomlyGeneratedMocks(insertValues);

            for (int i = 1; i < insertValues + 1; i++)
            {

                PackageInformationResponse? package = await _databaseMockOperations.GetOnePackage(i);

                Assert.NotNull(package);
                Assert.Equal(package.Id, i);

            }

        }

        //Status updates explanation:
        //      • Created → Can be changed to: Sent, Canceled     
        //      • Sent → Can be changed to: Accepted, Returned, Canceled
        //      • Returned → Can be changed to: Sent, Canceled
        //      • Accepted → Final status, cannot be changed
        //      • Canceled → Final status, cannot be changed


        [Theory]
        [InlineData("Canceled")]
        [InlineData("Sent, Returned, Canceled")]
        [InlineData("Sent, Returned, Sent, Accepted")]
        [InlineData("Sent, Returned, Sent, Canceled")]
        [InlineData("Sent, Accepted")]
        public async Task TestCorrectStatusUpdate(string statusUpdateSequence)
        {

            List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages = new List<List<StatusHistoryRequest>>();

            var statusHistoryRequestList = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList(statusUpdateSequence, 1);

            updateSequencesForSeveralPackages.Add(statusHistoryRequestList);


            _databaseMockOperations.InsertRandomlyGeneratedMocks(updateSequencesForSeveralPackages.Count());
            List<PackageInformationResponse> responses = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CheckIfInitialStatusesSetToCreated(responses);

            _databaseMockOperations.UpdatePackageStatus(statusHistoryRequestList);
            List<PackageInformationResponse> responsesAfterUpdate = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CheckIfStatusUpdated(responsesAfterUpdate, updateSequencesForSeveralPackages);
        }

        [Fact]
        public async Task TestCorrectStatusUpdate_SeveralDifferentPackagesSequences()
        {

            List<string> packageStatusUpdateSequencesStringRepresentations = new List<string>()
            {
                "Sent, Accepted",
                "Sent, Canceled",
                "Sent, Returned, Canceled",
                "Sent, Accepted",
                "Canceled",
                "Sent, Returned, Accepted"
            };

            List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages = new List<List<StatusHistoryRequest>>();

            for (int i = 0; i < packageStatusUpdateSequencesStringRepresentations.Count; i++)
            {
                int packageRef = i + 1;
                string updateSequenceStringRepresentation = packageStatusUpdateSequencesStringRepresentations[i];
                List<StatusHistoryRequest> requestSequence = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList(updateSequenceStringRepresentation, packageRef);
                updateSequencesForSeveralPackages.Add(requestSequence);

            }

            _databaseMockOperations.InsertRandomlyGeneratedMocks(updateSequencesForSeveralPackages.Count);
            List<PackageInformationResponse> responses = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CheckIfInitialStatusesSetToCreated(responses);

            foreach (List<StatusHistoryRequest> updates in updateSequencesForSeveralPackages)
            {
                _databaseMockOperations.UpdatePackageStatus(updates);
            }


            List<PackageInformationResponse> responsesAfterUpdate = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CheckIfStatusUpdated(responsesAfterUpdate, updateSequencesForSeveralPackages);
        }



    }
}
