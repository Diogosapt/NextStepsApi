using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextSteps.Adpater.SQL.Models;

namespace NextSteps.Adpater.SQL.Configuration
{
    internal class HobbiesEntityTypeConfiguration : IEntityTypeConfiguration<Hobbies>
    {
        public void Configure(EntityTypeBuilder<Hobbies> builder)
        {
            builder
              .HasKey(p => p.Id);
        }
    }
}