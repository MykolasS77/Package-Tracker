using ModelsLibrary.DTOs;

namespace DbServiceContracts
{
    public interface IPostMethods
    {
        /// <summary>
        /// Adds a new package to a database.
        /// </summary>
        /// <param name="packageItem">Details on new package provided by the client.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void PostPackage(PackageInformationRequest packageItem);
    }
}
