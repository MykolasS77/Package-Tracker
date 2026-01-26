using Microsoft.EntityFrameworkCore;
using PackageTracker.Server.Database;


namespace DatabaseOperationsTests
{
    public class TestClassBase
    {
        protected DatabaseContext _inMemoryContext;
        public TestClassBase()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            DatabaseContext context = new DatabaseContext(options);
            _inMemoryContext = context;

        }
    }
}
