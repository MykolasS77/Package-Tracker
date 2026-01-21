using ModelsLibrary.DTOs;
using ModelsLibrary.Models;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabaseOperationsTests
{
    public static class StatusHistoryRequestMockGenerator
    {    
  
        // Update sequence string value must contain values seperated by commas. For example: 'Sent, Canceled, Returned' or 'Sent, Accepted'
        public static List<StatusHistoryRequest> GenerateStatusHistoryRequestMockList(string updateSequenceString, int PackageRef)
        {

            string removedWhiteSpaces = Regex.Replace(updateSequenceString, @"\s", string.Empty);

            List<string> convertedRequestSequenceStringRepresentation = removedWhiteSpaces.Split(",").ToList();

            List<StatusHistoryRequest> statusHistoryRequestsMocks = new List<StatusHistoryRequest>();

            foreach (string update in convertedRequestSequenceStringRepresentation)
            {

                StatusHistoryRequest statusHistoryREquestMock = StatusHistoryRequestMock(update, PackageRef);
                statusHistoryRequestsMocks.Add(statusHistoryREquestMock);

            }

            return statusHistoryRequestsMocks;
        }


        public static StatusHistoryRequest StatusHistoryRequestMock(string Status, int PackageRef)
        {

            StatusHistoryRequest statusHistoryRequestMock = new StatusHistoryRequest()
            {
                Status = Status,
                PackageRef = PackageRef
            };



            return statusHistoryRequestMock;
        }




    }
}
