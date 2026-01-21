using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseOperationsTests
{
    public class MockRequestListGenerator
    {
        public static List<PackageRequestMock> GenerateMockRequestList(int insertValues)
        {
            List<PackageRequestMock> requestMocks = new List<PackageRequestMock>();

            for (int i = 0; i < insertValues; i++)
            {
                requestMocks.Add(new PackageRequestMock());
            }

            return requestMocks;
        }
    }
}
