using DatabaseOperationsTests.MockHelpers;
using ModelsLibrary.DTOs;
using ModelsLibrary.Models;
using Xunit;

namespace DatabaseOperationsTests.HelpersForTesting
{
    public class TestHelpers
    {

        public static void CompareResponseAgainstMock(List<PackageInformationResponse> packageResponses, List<PackageRequestMock> requestMocks)
        {
            Assert.Equal(packageResponses.Count, requestMocks.Count);

            for (int i = 0; i < packageResponses.Count; i++)
            {
                PackageInformationResponse response = packageResponses[i];
                PackageInformationRequest requestMock = requestMocks[i].GenerateMockObject();

                Assert.Equal(requestMock.Recipient?.FirstName, response.Recipient?.FirstName);
                Assert.Equal(requestMock.Recipient?.LastName, response.Recipient?.LastName);
                Assert.Equal(requestMock.Recipient?.Address, response.Recipient?.Address);
                Assert.Equal(requestMock.Recipient?.Phone, response.Recipient?.Phone);
                Assert.Equal(requestMock.Sender?.FirstName, response.Sender?.FirstName);
                Assert.Equal(requestMock.Sender?.LastName, response.Sender?.LastName);
                Assert.Equal(requestMock.Sender?.Address, response.Sender?.Address);
                Assert.Equal(requestMock.Sender?.Phone, response.Sender?.Phone);
            }



        }

        public static void CheckIfKeyValuesAscending(List<PackageInformationResponse> packageResponses)
        {
            for (int i = 0; i < packageResponses.Count - 1; i++)
            {
                Assert.Equal(packageResponses[i].Id + 1, packageResponses[i + 1].Id);

            }

        }


        public static void CheckIfInitialStatusesSetToCreated(List<PackageInformationResponse> responses)
        {

            foreach (PackageInformationResponse response in responses)
            {
                Assert.Single(response.TimeStampHistories);
                Assert.Equal(nameof(PackageStatus.Created), response.CurrentStatus);
             
            }

        }

        public static void CheckIfStatusUpdated(List<PackageInformationResponse> responsesAfterUpdate, List<List<StatusHistoryRequest>> statusHistoryRequestList)
        {

            if(responsesAfterUpdate == null || statusHistoryRequestList == null || responsesAfterUpdate.Count == 0 || statusHistoryRequestList.Count == 0)
            {
                throw new ArgumentNullException("One of the arguments has either a null value or is empty.");
            }

            foreach (PackageInformationResponse response in responsesAfterUpdate)
            {
                List<StatusHistoryResponse> statusChangesHistory = response.TimeStampHistories.ToList();
                statusChangesHistory.RemoveAt(0);
                List<List<StatusHistoryRequest>> requestsMocksThatMatchWithResponseById = statusHistoryRequestList.Where(p => p[0].PackageRef == response.Id).ToList();

                for (int i = 0; i < statusChangesHistory.Count; i++)
                {
                    
                    Assert.Equal(statusChangesHistory[i].Status, requestsMocksThatMatchWithResponseById[0][i].Status);

                }

            }

        }




    }
}
