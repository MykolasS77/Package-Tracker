using ModelsLibrary.DTOs;
using System;
using System.Collections.Generic;


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
            Random randomNumber = new Random();
            RandomPackageDetailsValues randomDetails = new RandomPackageDetailsValues();
            List<string> firstNames = randomDetails.FirstNames;
            List<string> lastNames = randomDetails.LastNames;
            List<string> adresses = randomDetails.Addresses;
            List<string> phoneNumbers = randomDetails.PhoneNumbers;

            _recipientFirstName = firstNames[randomNumber.Next(0, firstNames.Count)];
            _recipientLastName = lastNames[randomNumber.Next(0, lastNames.Count)];
            _senderFirstName = firstNames[randomNumber.Next(0, firstNames.Count)];
            _senderLastName = lastNames[randomNumber.Next(0, lastNames.Count)];
            _recipientPhone = phoneNumbers[randomNumber.Next(0, phoneNumbers.Count)];
            _senderPhone = phoneNumbers[randomNumber.Next(0, phoneNumbers.Count)];
            _recipientAdress = adresses[randomNumber.Next(0, adresses.Count)];
            _senderAdress = adresses[randomNumber.Next(0, adresses.Count)];

        }
     

    }
}
