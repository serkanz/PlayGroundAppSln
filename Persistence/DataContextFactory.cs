using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence
{
    internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            
            builder.UseNpgsql("Host=localhost;Port=5432;Database=playgrounddb;Username=playgrounduser;Password=**12qwas**");

            var context = new DataContext(builder.Options);
            return context;
        }
    }
}
