using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(k=>k.Id);

            builder.Property(l => l.Id).UseIdentityColumn();

            builder.Property(l => l.Name).IsRequired().HasMaxLength(50);

            builder.ToTable("Authors");
        }

    }
}
