using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace DatabaseOperationsTests.MockHelpers
{
    public class PackageRequestMock : MockGeneratorBase
    {

        public PackageRequestMock(
            string recipientFirstName,
            string recipientLastName,
            string senderFirstName,
            string senderLastName,
            string recipientPhone,
            string senderPhone,
            string recipientAdress,
            string senderAdress) : base()
        {


            _recipientFirstName = recipientFirstName;
            _recipientLastName = recipientLastName;
            _senderFirstName = senderFirstName;
            _senderLastName = senderLastName;
            _recipientPhone = recipientPhone;
            _senderPhone = senderPhone;
            _recipientAdress = recipientAdress;
            _senderAdress = senderAdress;
        
        
        }

        public PackageRequestMock() {}

        public PackageInformationRequest GenerateMockObject()
        {
            PackageInformationRequest mockRequest = new PackageInformationRequest()
            {
                Recipient = new RecipientDTO()
                {
                    FirstName = _recipientFirstName,
                    LastName = _recipientLastName,
                    Address = _recipientAdress,
                    Phone = _recipientPhone
                },
                Sender = new SenderDTO()
                {
                    FirstName = _senderFirstName,
                    LastName = _senderLastName,
                    Address = _senderAdress,
                    Phone = _senderPhone
                },
                CurrentStatus = nameof(PackageStatus.Created),
            };

            return mockRequest;
        }
    }
}
