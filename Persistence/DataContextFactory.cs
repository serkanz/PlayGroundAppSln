using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;

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
