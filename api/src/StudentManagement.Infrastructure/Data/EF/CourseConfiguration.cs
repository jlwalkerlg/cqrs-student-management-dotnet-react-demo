using StudentManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentManagement.Infrastructure.Data.EF
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.OwnsOne(x => x.Title, m =>
            {
                m.Property(y => y.Title).HasColumnName("Title").IsRequired();
                m.HasIndex(y => y.Title).IsUnique();
            });

            builder.OwnsOne(x => x.Credits, m =>
            {
                m.Property(y => y.Amount).HasColumnName("Credits").IsRequired();
            });
        }
    }
}
