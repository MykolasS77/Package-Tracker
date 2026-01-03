
using ModelsLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;


namespace ModelsLibrary.Validation
{
    public class ValidationMethods
    {
        public static void CheckIfNullOrNegative(long id)
        {
            if (id == 0 || id < 0)
            {
                throw new ArgumentException("Id cannot be a negative number or 0");
            }
        }

        public static void ValidateStatusFilterValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("String value for enum conversion not provided.");
            }

            bool validValue = CheckIfFilterValueExists(value);

            if (!validValue)
            {
                throw new ArgumentException($"Value: {value} did not match any cases.");
            }


        }

        private static bool CheckIfFilterValueExists(string value)
        {
            foreach (int i in Enum.GetValues(typeof(PackageStatus)))
            {
                string? packageStatusValue = Enum.GetName(typeof(PackageStatus), i);

                if (packageStatusValue != null && packageStatusValue == value)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
