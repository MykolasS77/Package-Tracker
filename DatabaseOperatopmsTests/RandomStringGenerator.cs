using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DatabaseOperationsTests
{
    public class RandomStringGenerator
    {
        
        public static string GetRandomString(int wantedStringLength)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return BuildRandomString(wantedStringLength, chars);
        }

        public static string GetRandomNumberSequenceString(int wantedStringLength)
        {
            string numberChars = "0123456789";

            return BuildRandomString(wantedStringLength, numberChars);
        }

        private static string BuildRandomString(int wantedStringLength, string chars)
        {

            Random randomNumber = new Random();
            StringBuilder randomString = new StringBuilder();

            for (int i = 0; i < wantedStringLength; i++)
            {

                randomString.Append(chars[randomNumber.Next(0, chars.Length)]);
            }

            return randomString.ToString();

        }



    }
}
