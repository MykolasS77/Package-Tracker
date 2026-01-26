using DbServiceContracts;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Enum_Conversion
{
    public class PackageStatusMethods
    {
        private readonly IValidationMethods _validationMethods; 
        public PackageStatusMethods(IValidationMethods validationMethods)
        {
            _validationMethods = validationMethods;
        }
        public PackageStatus TryStringToEnumConvert(string value)
        {
            _validationMethods.ValidateStatusFilterValue(value);

            return (PackageStatus) Enum.Parse(typeof(PackageStatus), value);

        }

      
    }
  


}
