namespace PackageTracker.Server.Database.CRUD_Operations
{
    public abstract class DatabaseLogic
    {
        protected readonly DatabaseContext _context;
        public DatabaseLogic(DatabaseContext context)
        {
            _context = context;
        }

        protected void TrySaveChanges(string errorMessage = "Error while trying to save changes after database operation.")
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), errorMessage);
                throw;
            }
        }


    }
}
