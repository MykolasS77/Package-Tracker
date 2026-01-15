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

        public BusinessLogicTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging()
            .Options;

            DatabaseContext context = new DatabaseContext(options);
            _databaseService = new DatabaseLogic(context);
        }

        [Fact]
        public async Task PostSinglePackage_ManuallyAddedValues()
        {
            PersonInformationMockData personInformationMockData = new PersonInformationMockData();
            PersonInformationMockData personInformationMockData2 = new PersonInformationMockData()
            {
                recipientFirstName = "Jon",
                recipientLastName = "Snow",
                recipientAdress = "The Wall",
                recipientPhone = "1858898798",
                senderFirstName = "Ned",
                senderLastName = "Stark",
                senderAdress = "The North",
                senderPhone = "12345678988"
            };

            List<PackageInformationRequest> requestMocks = new List<PackageInformationRequest>()
            {
                MockGenerator.CreateMockInstance(personInformationMockData),
                MockGenerator.CreateMockInstance(personInformationMockData2)
            };

            foreach (PackageInformationRequest requestMock in requestMocks)
            {

                _databaseService.PostPackage(requestMock);

            }

            List<PackageInformationResponse> packagesResponse = await _databaseService.GetAllPackagesResponse();

            bool testCase = TestHelpers.CompareResponseAgainstMock(packagesResponse, requestMocks);
            Assert.True(testCase);

        }

        [Fact]
        public async Task PostSinglePackage_ProperDetails_MockedData()
        {
            List<PackageInformationRequest> requestMocks = new List<PackageInformationRequest>();

            PackageInformationRequest requestMock = MockGenerator.CreateMockInstance(new PersonInformationMockData());

            requestMocks.Add(requestMock);

            _databaseService.PostPackage(requestMock);

            List<PackageInformationResponse> packagesResponse = await _databaseService.GetAllPackagesResponse();

            bool testCase = TestHelpers.CompareResponseAgainstMock(packagesResponse, requestMocks);
            Assert.True(testCase);


        }


        [Fact]
        public async Task PostSeveralPackages_10_RandomlyGeneratedValues()
        {
            int insertValues = 10;
            bool testCase = await TestHelpers.TestAgainstMockedValues(insertValues, _databaseService);
            Assert.True(testCase);

        }

        [Fact]
        public async Task PostSeveralPackages_50_RandomlyGeneratedValues()
        {

            int insertValues = 50;

            bool testCase = await TestHelpers.TestAgainstMockedValues(insertValues, _databaseService);
            Assert.True(testCase);

        }

        [Fact]
        public async Task PostSeveralPackages_100_RandomlyGeneratedValues()
        {

            int insertValues = 100;

            bool testCase = await TestHelpers.TestAgainstMockedValues(insertValues, _databaseService);
            Assert.True(testCase);

        }

        [Fact]
        public async Task PostSeveralPackages_500_RandomlyGeneratedValues()
        {

            int insertValues = 500;

            bool testCase = await TestHelpers.TestAgainstMockedValues(insertValues, _databaseService);
            Assert.True(testCase);

        }



    }
}
