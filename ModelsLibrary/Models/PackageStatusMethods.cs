using ModelsLibrary.Validation;


namespace ModelsLibrary.Models
{
    public static class PackageStatusMethods
    {
        public static PackageStatus TryStringToEnumConvert(string value)
        {
            ValidationMethods.ValidateStatusFilterValue(value);

            return (PackageStatus) Enum.Parse(typeof(PackageStatus), value);

        }

      
    }
  


}
