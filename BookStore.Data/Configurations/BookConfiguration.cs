using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(b => b.Id).UseIdentityColumn();
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.HasOne(b => b.Author).WithMany(b => b.Books).HasForeignKey(b => b.AuthorId);
            builder.ToTable("Books");
        }
    }
}
