using DatabaseOperationsTests.HelpersForTesting;
using DatabaseOperationsTests.MockHelpers;
using DatabaseOperationsTests.TestDatabaseOperationsHelpers;
using DbServiceContracts;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;
using PackageTracker.Server.Database.CRUD_Operations;
using Xunit;

namespace DatabaseOperationsTests
{
    public class StatusUpdateTests: TestClassBase
    {

        private readonly IGetMethods _getMethods;
        private readonly TestDatabaseOperations _testDatabaseOperations;

        public StatusUpdateTests()
        {
            _getMethods = new GetMethods(_inMemoryContext);
            _testDatabaseOperations = new TestDatabaseOperations(_inMemoryContext);
        }

        //Status updates explanation:
        //      • Created → Can be changed to: Sent, Canceled     
        //      • Sent → Can be changed to: Accepted, Returned, Canceled
        //      • Returned → Can be changed to: Canceled
        //      • Accepted → Final status, cannot be changed
        //      • Canceled → Final status, cannot be changed

        [Theory]
        [InlineData(nameof(PackageStatus.Canceled))]
        [InlineData($"{nameof(PackageStatus.Sent)}, {nameof(PackageStatus.Returned)}, {nameof(PackageStatus.Canceled)}")]
        [InlineData($"{nameof(PackageStatus.Sent)}, {nameof(PackageStatus.Accepted)}")]
        public async Task TestCorrectStatusUpdate(string statusUpdateSequence)
        {

            List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages = new List<List<StatusHistoryRequest>>()
            {
                StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList(statusUpdateSequence, 1)
            };


            _testDatabaseOperations.AutoAddMockedPackages(updateSequencesForSeveralPackages.Count());
            List<PackageInformationResponse> responses = await _getMethods.GetAllPackagesResponse();

            TestHelpers.CheckIfInitialStatusesSetToCreated(responses);

            await _testDatabaseOperations.ExcecuteStatusUpdateSequenceForSeveralPackages(updateSequencesForSeveralPackages);
            List<PackageInformationResponse> responsesAfterUpdate = await _getMethods.GetAllPackagesResponse();

            TestHelpers.CheckIfStatusUpdated(responsesAfterUpdate, updateSequencesForSeveralPackages);
        }

        [Fact]
        public async Task TestCorrectStatusUpdate_SeveralDifferentPackagesSequences()
        {

            List<string> packageStatusUpdateSequencesStringRepresentations = new List<string>()
            {
                $"{nameof(PackageStatus.Sent)}, {nameof(PackageStatus.Accepted)}",
                $"{nameof(PackageStatus.Sent)}, {nameof(PackageStatus.Canceled)}",
                $"{nameof(PackageStatus.Sent)}, {nameof(PackageStatus.Returned)}, {nameof(PackageStatus.Canceled)}",
                $"{nameof(PackageStatus.Canceled)}"
            };

            List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages = _testDatabaseOperations.GenerateUpdateSequenceForSeveralDifferentPackages(packageStatusUpdateSequencesStringRepresentations);

            _testDatabaseOperations.AutoAddMockedPackages(updateSequencesForSeveralPackages.Count);

            await _testDatabaseOperations.ExcecuteStatusUpdateSequenceForSeveralPackages(updateSequencesForSeveralPackages);

            List<PackageInformationResponse> responsesAfterUpdate = await _getMethods.GetAllPackagesResponse();

            TestHelpers.CheckIfStatusUpdated(responsesAfterUpdate, updateSequencesForSeveralPackages);
        }

        [Theory]
        [InlineData(nameof(PackageStatus.Created))]
        [InlineData(nameof(PackageStatus.Returned))]
        [InlineData(nameof(PackageStatus.Accepted))]
        public async Task TestIncorrectStatusUpdate_FromCreated(string incorrectUpdateString)
        {

            int packageRef = 1;
            _testDatabaseOperations.AutoAddMockedPackages((int)packageRef);

            await Assert.ThrowsAsync<Exception>(() => _testDatabaseOperations.AddTimestampForSpecificPackage(incorrectUpdateString, packageRef));

        }

        [Theory]
        [InlineData(nameof(PackageStatus.Sent))]
        [InlineData(nameof(PackageStatus.Created))]
        public async Task TestIncorrectStatusUpdate_FromSent(string incorrectUpdateString)
        {

            int packageRef = 1;
            _testDatabaseOperations.AutoAddMockedPackages((int)packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Sent), packageRef);

            await Assert.ThrowsAsync<Exception>(() => _testDatabaseOperations.AddTimestampForSpecificPackage(incorrectUpdateString, packageRef));

        }

        [Theory]
        [InlineData(nameof(PackageStatus.Created))]
        [InlineData(nameof(PackageStatus.Returned))]
        [InlineData(nameof(PackageStatus.Accepted))]
        public async Task TestIncorrectStatusUpdate_FromReturned(string incorrectUpdateString)
        {

            int packageRef = 1;
            _testDatabaseOperations.AutoAddMockedPackages((int)packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Sent), packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Returned), packageRef);

            await Assert.ThrowsAsync<Exception>(() => _testDatabaseOperations.AddTimestampForSpecificPackage(incorrectUpdateString, packageRef));

        }


        [Theory]
        [InlineData(nameof(PackageStatus.Created))]
        [InlineData(nameof(PackageStatus.Sent))]
        [InlineData(nameof(PackageStatus.Returned))]
        [InlineData(nameof(PackageStatus.Accepted))]
        [InlineData(nameof(PackageStatus.Canceled))]
        public async Task TestIncorrectStatusUpdate_FromAccepted(string incorrectUpdateString)
        {

            int packageRef = 1;
            _testDatabaseOperations.AutoAddMockedPackages((int)packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Sent), packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Accepted), packageRef);

            await Assert.ThrowsAsync<Exception>(() => _testDatabaseOperations.AddTimestampForSpecificPackage(incorrectUpdateString, packageRef));

        }

        [Theory]
        [InlineData(nameof(PackageStatus.Created))]
        [InlineData(nameof(PackageStatus.Sent))]
        [InlineData(nameof(PackageStatus.Returned))]
        [InlineData(nameof(PackageStatus.Accepted))]
        [InlineData(nameof(PackageStatus.Canceled))]
        public async Task TestIncorrectStatusUpdate_Canceled(string incorrectUpdateString)
        {

            int packageRef = 1;
            _testDatabaseOperations.AutoAddMockedPackages((int)packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Sent), packageRef);
            await _testDatabaseOperations.AddTimestampForSpecificPackage(nameof(PackageStatus.Canceled), packageRef);

            await Assert.ThrowsAsync<Exception>(() => _testDatabaseOperations.AddTimestampForSpecificPackage(incorrectUpdateString, packageRef));

        }
    }
}
