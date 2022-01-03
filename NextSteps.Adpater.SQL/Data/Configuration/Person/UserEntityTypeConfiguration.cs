using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextSteps.Adpater.SQL.Models;

namespace NextSteps.Adpater.SQL.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
              .HasKey(c => c.Id);
        }
    }
}