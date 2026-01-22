using ModelsLibrary;

namespace DatabaseOperationsTests
{
    public abstract class MockGeneratorBase
    {
        protected string _recipientFirstName;
        protected string _recipientLastName;
        protected string _senderFirstName;
        protected string _senderLastName;
        protected string _recipientPhone;
        protected string _senderPhone;
        protected string _recipientAdress;
        protected string _senderAdress;


        //Insert randomly generated values if no arguments provided
        public MockGeneratorBase()
        {
            RandomPackageDetailsValues randomValues = new RandomPackageDetailsValues();

            _recipientFirstName = randomValues.GetRandomFirstName();
            _recipientLastName = randomValues.GetRandomLastName();
            _senderFirstName = randomValues.GetRandomFirstName();
            _senderLastName = randomValues.GetRandomLastName();
            _recipientPhone = randomValues.GetRandomPhoneNumber();
            _senderPhone = randomValues.GetRandomPhoneNumber();
            _recipientAdress = randomValues.GetRandomAddress();
            _senderAdress = randomValues.GetRandomAddress();

        }


    }
}
