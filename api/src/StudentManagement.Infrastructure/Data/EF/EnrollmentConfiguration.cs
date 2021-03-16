using StudentManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Courses;

namespace StudentManagement.Infrastructure.Data.EF
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasOne(typeof(Course)).WithMany().IsRequired().HasForeignKey("CourseId");

            builder
                .Property(x => x.Grade)
                .HasConversion<string>()
                .HasMaxLength(1);
        }
    }
}
