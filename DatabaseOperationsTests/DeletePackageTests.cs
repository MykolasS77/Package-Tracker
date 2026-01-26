using DatabaseOperationsTests.TestDatabaseOperationsHelpers;
using DbServiceContracts;
using ModelsLibrary.DTOs;
using PackageTracker.Server.Database.CRUD_Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DatabaseOperationsTests
{
    public class DeletePackageTests: TestClassBase
    {
        private readonly IGetMethods _getMethods;
        private readonly IDeleteMethods _deleteMethods;
        private readonly TestDatabaseOperations _testDatabaseOperations;


        public DeletePackageTests()
        {
            _getMethods = new GetMethods(_inMemoryContext);
            _deleteMethods = new DeletePackageMethods(_inMemoryContext);
            _testDatabaseOperations = new TestDatabaseOperations(_inMemoryContext);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task DeleteMethodTest_DeleteOnePackage(int insertNumberOfPackages)
        {
            Random randomNumber = new Random();

            int packageId = (int)randomNumber.Next(1, insertNumberOfPackages);

            _testDatabaseOperations.AutoAddMockedPackages(insertNumberOfPackages);

            PackageInformationResponse? packageForUpcomingDelete = await _getMethods.GetOnePackageResponse(packageId);
            List<PackageInformationResponse>? packages = await _getMethods.GetAllPackagesResponse();

            Assert.Equal(insertNumberOfPackages, packages.Count());
            Assert.Contains(packages, item => item.Id == packageId);


            _deleteMethods.DeletePackage(packageId);
            List<PackageInformationResponse>? packagesAfterDelete = await _getMethods.GetAllPackagesResponse();

            Assert.DoesNotContain(packagesAfterDelete, item => item.Id == packageId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(30)]
        public async Task DeleteMethodTest_PackageNotFoundError(int nonExistentPackageId)
        {
            Assert.Throws<Exception>(() => _deleteMethods.DeletePackage(nonExistentPackageId));
        }
    }
}
