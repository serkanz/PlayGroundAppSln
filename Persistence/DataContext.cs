using System.Linq.Expressions;
using Domain;
using Domain.Abstractions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Project> Projects { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Expression<Func<IEntity, bool>>? expression = e => e.IsDeleted == false;

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(type.ClrType))
                {
                    var newParam = Expression.Parameter(type.ClrType);
                    var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);

                    modelBuilder.Entity(type.Name).HasQueryFilter(Expression.Lambda(newBody, newParam));
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetDates();

            return base.SaveChanges();
        }

        private void SetDates()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is AbstractEntity && e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);
            foreach (var entityEntry in entries)
            {
                var entityEntryEntity = ((AbstractEntity)entityEntry.Entity);
                entityEntryEntity.DateUpdated = DateTime.UtcNow;
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntryEntity.DateCreated = entityEntryEntity.DateUpdated;
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntryEntity.IsDeleted = true;
                    entityEntry.State = EntityState.Modified;
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            SetDates();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
