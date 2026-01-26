using DatabaseOperationsTests.MockHelpers;
using DbServiceContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using ModelsLibrary.DTOs;
using PackageTracker.Server.Database;
using PackageTracker.Server.Database.CRUD_Operations;
using PackageTracker.Server.Database.Validation;
using Xunit.Sdk;

namespace DatabaseOperationsTests.TestDatabaseOperationsHelpers
{
    public class TestDatabaseOperations
    {
        
        private readonly IPostMethods _postMethods;
        private readonly IUpdateMethods _updateMethods;
        public TestDatabaseOperations(DatabaseContext _inMemoryContext)
        {
            _postMethods = new PostNewPackageMethods(_inMemoryContext);
            _updateMethods = new UpdatePackageStatusMethods(_inMemoryContext, new ValidationMethods(_inMemoryContext));
        }

        public void AddPredefinedMocks(List<PackageRequestMock> requestMocks)
        {

            foreach (PackageRequestMock requestMock in requestMocks) {

                _postMethods.PostPackage(requestMock.GenerateMockObject());

            }

        }

        public void AutoAddMockedPackages(int insertMocks)
        {
            List<PackageRequestMock> mockList = MockRequestListGenerator.GenerateMockRequestList(insertMocks);

            foreach (PackageRequestMock requestMock in mockList)
            {
                _postMethods.PostPackage(requestMock.GenerateMockObject());
            }

        }

        public async Task ExcecuteStatusUpdateSequenceForSeveralPackages(List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages)
        {     

            foreach (List<StatusHistoryRequest> updates in updateSequencesForSeveralPackages)
            {
                ExcecuteUpdateSequenceForOnePackage(updates);
            }

        }

        public  List<List<StatusHistoryRequest>> GenerateUpdateSequenceForSeveralDifferentPackages(List<string> packageStatusUpdateSequencesStringRepresentations)
        {
            List<List<StatusHistoryRequest>> updateSequencesForSeveralPackages = new List<List<StatusHistoryRequest>>();

            for (int i = 0; i < packageStatusUpdateSequencesStringRepresentations.Count; i++)
            {
                int packageRef = i + 1;
                string updateSequenceStringRepresentation = packageStatusUpdateSequencesStringRepresentations[(int)i];
                List<StatusHistoryRequest> requestSequence = StatusHistoryRequestMockGenerator.GenerateStatusHistoryRequestMockList(updateSequenceStringRepresentation, packageRef);
                updateSequencesForSeveralPackages.Add(requestSequence);

            }

            return updateSequencesForSeveralPackages;
        }


        public void ExcecuteUpdateSequenceForOnePackage(List<StatusHistoryRequest> statusHistoryRequestList)
        {
            foreach (StatusHistoryRequest request in statusHistoryRequestList)
            {
                _updateMethods.AddTimestamp(request);
            }
        }

        public async Task AddTimestampForSpecificPackage(string updateString, int PackageRef)
        {
            StatusHistoryRequest request = new StatusHistoryRequest()
            {
                Status = updateString,
                PackageRef = PackageRef
            };

            _updateMethods.AddTimestamp(request);

        }

    }
}
