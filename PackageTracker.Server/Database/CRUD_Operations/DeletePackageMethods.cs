using DbServiceContracts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Database.CRUD_Operations
{
    public class DeletePackageMethods : DatabaseLogic, IDeleteMethods
    {
        public DeletePackageMethods(DatabaseContext context) : base(context)
        {
        }
        public void DeletePackage(long id)
        {


            PackageInformation? package = _context.PackageInformations.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                throw new Exception($" Package with id {id} not found.");
            }

            _context.PackageInformations.Remove(package);

            TrySaveChanges("Error while deleting a package.");

        }
    }
}
