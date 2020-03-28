using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {

        public CatalogContext(DbContextOptions<CatalogContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("CreatedDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedDate").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder
             .Model
             .GetEntityTypes()
             .SelectMany(e => e.GetProperties()
               .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

    }
}
