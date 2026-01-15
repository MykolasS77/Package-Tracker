using DatabaseServiceContracts;
using ModelsLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace DatabaseOperationsTests
{
    public class TestHelpers
    {  
        public async static Task<bool> TestAgainstMockedValues(int insertValues, IDatabaseService _databaseService)
        {

            List<PackageInformationRequest> requestMocks = MockGenerator.GenerateMockRequestList(insertValues);
            List<bool> testCases = new List<bool>();

            AddMockedPackages(requestMocks, _databaseService);

            List<PackageInformationResponse> packagesResponse = await _databaseService.GetAllPackagesResponse();

            testCases.Add(CompareResponseAgainstMock(packagesResponse, requestMocks));
            testCases.Add(CheckIfKeyValuesAscending(packagesResponse));

            foreach (bool testCase in testCases) {
                if (!testCase) {
                    return false;
                }  
            }

            return true;
        }

        public static void AddMockedPackages(List<PackageInformationRequest> requestMocks, IDatabaseService _databaseService)
        {
            for (int i = 0; i < requestMocks.Count; i++)
            {
                _databaseService.PostPackage(requestMocks[i]);
            }

        }

        public static bool CompareResponseAgainstMock(List<PackageInformationResponse> packageResponses, List<PackageInformationRequest> requestMocks)
        {

            for (int i = 0; i < packageResponses.Count; i++)
            {
                if (packageResponses[i] is not PackageInformationResponse ||
                   packageResponses.Count != requestMocks.Count ||
                   packageResponses[i].Recipient?.FirstName != requestMocks[i].Recipient?.FirstName ||
                   packageResponses[i].Recipient?.LastName != requestMocks[i].Recipient?.LastName ||
                   packageResponses[i].Recipient?.Address != requestMocks[i].Recipient?.Address ||
                   packageResponses[i].Recipient?.Phone != requestMocks[i].Recipient?.Phone ||
                   packageResponses[i].Sender?.FirstName != requestMocks[i].Sender?.FirstName ||
                   packageResponses[i].Sender?.LastName != requestMocks[i].Sender?.LastName ||
                   packageResponses[i].Sender?.Address != requestMocks[i].Sender?.Address ||
                   packageResponses[i].Sender?.Phone != requestMocks[i].Sender?.Phone
                    )
                    return false;                   
            }

            return true;

        }
        

        public static bool CheckIfKeyValuesAscending(List<PackageInformationResponse> packageResponses)
        {
            for (int i = 0;  i < packageResponses.Count - 1; i++)
            {
                if (packageResponses[i].Id + 1 != packageResponses[i + 1].Id)
                {
                    return false;
                }
            }
            return true; 
        }


    }
}
