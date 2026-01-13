using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Configurations
{
    public class Configuration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

            builder.Property(x => x.Profession).IsRequired().HasMaxLength(256);

            builder.Property(x => x.ImagePath).IsRequired();

        }
    }
}
