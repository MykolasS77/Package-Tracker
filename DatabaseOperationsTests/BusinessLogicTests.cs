using DatabaseServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DTOs;
using PackageTracker.Server.Database;
using Xunit;

namespace DatabaseOperationsTests
{
    public class BusinessLogicTests
    {
        private readonly IDatabaseService _databaseService;

        private DatabaseMockOperations _databaseMockOperations;
        public BusinessLogicTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            DatabaseContext context = new DatabaseContext(options);
            _databaseService = new DatabaseLogic(context);
            _databaseMockOperations = new DatabaseMockOperations(_databaseService);

        }

        [Fact]
        public async Task PostSinglePackage_ManuallyAddedValues()
        {

            List<PackageRequestMock> requestMocks = new List<PackageRequestMock>() {

                new PackageRequestMock(),
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

            foreach (PackageRequestMock requestMock in requestMocks)
            {

                _databaseService.PostPackage(requestMock.GenerateMockObject());

            }

            List<PackageInformationResponse> packagesResponse = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CompareResponseAgainstMock(packagesResponse, requestMocks);

        }

        [Fact]
        public async Task PostSinglePackage_ProperDetails_MockedData()
        {

            List<PackageRequestMock> requestMocks = new List<PackageRequestMock>()
            {
                new PackageRequestMock()
            };
          
            _databaseService.PostPackage(requestMocks[0].GenerateMockObject());

            List<PackageInformationResponse> packagesResponse = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CompareResponseAgainstMock(packagesResponse, requestMocks);



        }

        [Theory]
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

            List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages = new List<List<StatusHistoryRequest>>();
   
            var statusHistoryRequestList1 = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList("Sent, Accepted", 1);
            var statusHistoryRequestList2 = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList("Sent, Canceled", 2);
            var statusHistoryRequestList3 = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList("Sent, Returned, Canceled", 3);
            var statusHistoryRequestList4 = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList("Sent, Accepted", 4);
            var statusHistoryRequestList5 = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList("Canceled", 5);
            var statusHistoryRequestList6 = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList("Sent, Returned, Accepted", 6);

            updateSequencesForSeveralPackages.Add(statusHistoryRequestList1);
            updateSequencesForSeveralPackages.Add(statusHistoryRequestList2);
            updateSequencesForSeveralPackages.Add(statusHistoryRequestList3);
            updateSequencesForSeveralPackages.Add(statusHistoryRequestList4);
            updateSequencesForSeveralPackages.Add(statusHistoryRequestList5);
            updateSequencesForSeveralPackages.Add(statusHistoryRequestList6);


            _databaseMockOperations.InsertRandomlyGeneratedMocks(updateSequencesForSeveralPackages.Count);
            List<PackageInformationResponse> responses = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CheckIfInitialStatusesSetToCreated(responses);

            foreach(List<StatusHistoryRequest> updates in updateSequencesForSeveralPackages)
            {
                _databaseMockOperations.UpdatePackageStatus(updates);
            }

            
            List<PackageInformationResponse> responsesAfterUpdate = await _databaseMockOperations.GetAllPackagesResponse();

            TestHelpers.CheckIfStatusUpdated(responsesAfterUpdate, updateSequencesForSeveralPackages);
        }



    }
}
