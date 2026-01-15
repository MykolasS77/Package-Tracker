using ModelsLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseOperationsTests
{
    public class MockGenerator
    {
        public static List<PackageInformationRequest> GenerateMockRequestList(int insertValues)
        {
            List<PackageInformationRequest> requestMocks = new List<PackageInformationRequest>();

            for (int i = 0; i < insertValues; i++)
            {
                PackageInformationRequest singleRequestMock = GenerateMockObjectWithRandomStringValues();
                requestMocks.Add(singleRequestMock);
            }

            return requestMocks;
        }
        public static PackageInformationRequest GenerateMockObjectWithRandomStringValues()
        {

            Random randomNumber = new Random();
            PersonInformationMockData personInformationMockData = new PersonInformationMockData()
            {
                recipientFirstName = RandomStringGenerator.GetRandomString(randomNumber.Next(5, 10)),
                recipientLastName = RandomStringGenerator.GetRandomString(randomNumber.Next(5, 10)),
                senderFirstName = RandomStringGenerator.GetRandomString(randomNumber.Next(5, 10)),
                senderLastName = RandomStringGenerator.GetRandomString(randomNumber.Next(5, 10)),
                recipientPhone = RandomStringGenerator.GetRandomNumberSequenceString(randomNumber.Next(7, 16)),
                senderPhone = RandomStringGenerator.GetRandomNumberSequenceString(randomNumber.Next(7, 16)),
                recipientAdress = AddressMock(randomNumber.Next(5, 10), randomNumber.Next(5, 10)),
                senderAdress = AddressMock(randomNumber.Next(5, 10), randomNumber.Next(5, 10))
            };

            PackageInformationRequest packageInformationRequestMock = CreateMockInstance(personInformationMockData);

            return packageInformationRequestMock;

        }
        public static string AddressMock(int wantedStringLength, int wantedNumberLength)
        {
            StringBuilder adressMock = new StringBuilder();

            string randomString = RandomStringGenerator.GetRandomString(wantedStringLength);
            string randomNumberStringRepresentation = RandomStringGenerator.GetRandomNumberSequenceString(wantedNumberLength);

            adressMock.Append(randomString);
            adressMock.Append(" ");
            adressMock.Append(randomNumberStringRepresentation);

            return adressMock.ToString();

        }

        public static PackageInformationRequest CreateMockInstance(PersonInformationMockData mockData)
        {
            RecipientDTO recipientDTO = new RecipientDTO()
            {
                FirstName = mockData.recipientFirstName,
                LastName = mockData.recipientLastName,
                Address = mockData.recipientAdress,
                Phone = mockData.recipientPhone

            };

            SenderDTO senderDTO = new SenderDTO()
            {

                FirstName = mockData.senderFirstName,
                LastName = mockData.senderLastName,
                Address = mockData.senderAdress,
                Phone = mockData.senderPhone

            };

            PackageInformationRequest request = new PackageInformationRequest()
            {
                Recipient = recipientDTO,
                Sender = senderDTO,
                CurrentStatus = "Created"
            };

            return request;
        }
    }
}
