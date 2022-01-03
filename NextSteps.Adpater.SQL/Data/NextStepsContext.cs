using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NextSteps.Adpater.SQL.Configuration;
using NextSteps.Adpater.SQL.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Adpater.SQL.Data
{
    public sealed class NextStepsContext : DbContext
    {
        public string PersonId { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public NextStepsContext(DbContextOptions<NextStepsContext> options) : base(options)
        {
            PersonId = "NextSteps User";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("persons");

            modelBuilder.ApplyConfiguration(new HobbiesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var added = ChangeTracker.Entries()
                .Where(t => t.Entity is IEntity && t.State == EntityState.Added)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in added)
            {
                var track = entity as IEntity;
                track.CreateDate = DateTime.Now;
                track.CreatedBy = PersonId;
                track.UpdateDate = DateTime.Now;
                track.UpdatedBy = PersonId;
            }

            var modified = ChangeTracker.Entries()
                .Where(t => t.Entity is IEntity && t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in modified)
            {
                var track = entity as IEntity;
                Entry(track).Property(x => x.CreateDate).IsModified = false;
                Entry(track).Property(x => x.CreatedBy).IsModified = false;
                track.UpdateDate = DateTime.Now;
                track.UpdatedBy = PersonId;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}