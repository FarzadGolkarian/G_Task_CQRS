using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using G_Task.Domain.Common;
using G_Task.Domain;

namespace G_Task.Persistence
{
    public class G_TaskDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public G_TaskDbContext(DbContextOptions<G_TaskDbContext> options, IHttpContextAccessor httpContextAccessor)
                : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(G_TaskDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            ConfigAuditEntity(modelBuilder);

            ConfigModifiedAuditEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetAudit();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            SetAudit();

            return base.SaveChanges();
        }

        private static void ConfigAuditEntity(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => typeof(IAuditEntity)
                .IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entity.Name)
                    .Property(nameof(IAuditEntity.CreateBy))
                    .HasMaxLength(100);

                modelBuilder.Entity(entity.Name)
                    .Property(nameof(IAuditEntity.CreateDate))
                    .IsRequired();
            }
        }

        private static void ConfigModifiedAuditEntity(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(IModifiedAuditEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entity.Name)
                    .Property(nameof(IModifiedAuditEntity.ModifiedBy))
                    .IsRequired(false).HasMaxLength(100);

                modelBuilder.Entity(entity.Name)
                    .Property(nameof(IModifiedAuditEntity.ModifiedDate))
                    .IsRequired(false);
            }
        }

        void SetAudit()
        {
            var addedAuditedEntities =
                ChangeTracker.Entries<IAuditEntity>()
                             .Where(p => p.State == EntityState.Added)
                             .Select(p => p.Entity);

            var modifiedAuditedEntities =
                ChangeTracker.Entries<IFullAuditEntity>()
                             .Where(p => p.State == EntityState.Modified)
                             .Select(p => p.Entity);

            var now = DateTime.UtcNow;
            
            
            string? user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            
            //For Test
            user = "admin";

            foreach (var added in addedAuditedEntities)
            {
                added.CreateDate = now;

                if (added is IUser u)
                {
                    if (u != null)
                    {
                        added.CreateBy = u.UserName;

                        continue;
                    }
                }
                added.CreateBy = user ?? throw new Common.Exceptions.NotFoundException(nameof(u.UserName),0);
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.ModifiedDate = now;

                modified.ModifiedBy = user ?? throw new Common.Exceptions.NotFoundException(nameof(user), 0);
               
            }
        }

    }
}
