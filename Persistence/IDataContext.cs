using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public interface IDataContext
{
    DbSet<Project> Projects { get; set; }
    int SaveChanges();
    
}