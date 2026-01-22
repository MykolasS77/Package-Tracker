namespace DbServiceContracts
{
    public interface IDeleteMethods
    {
        /// <summary>
        /// Deletes one package from the database.
        /// </summary>
        /// <param name="id">PackageId parameter</param>
        public void DeletePackage(long id);
    }
}
