using StudentManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentManagement.Infrastructure.Data.EF
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.OwnsOne(x => x.Name, m =>
            {
                m.Property(y => y.Name).HasColumnName("Name").IsRequired();
            });

            builder.OwnsOne(x => x.Email, m =>
            {
                m.Property(y => y.Address).HasColumnName("Email").IsRequired();
                m.HasIndex(y => y.Address).IsUnique();
            });

            builder.HasMany(x => x.Enrollments).WithOne().IsRequired();
            builder.Metadata
                .FindNavigation("Enrollments")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
