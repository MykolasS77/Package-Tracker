namespace ModelsLibrary.Models
{
    public static class PackageStatusMethods
    {
        public static PackageStatus StringToEnumConvert(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("String value for enum conversion not provided.");
            }

            switch (value)
            {
                case "Created":
                    return PackageStatus.Created;
                case "Sent":
                    return PackageStatus.Sent;
                case "Accepted":
                    return PackageStatus.Accepted;
                case "Canceled":
                    return PackageStatus.Canceled;
                case "Returned":
                    return PackageStatus.Returned;
            }

            throw new ArgumentException($"Value: {value} did not match any cases.");
        }
    }
  


}
