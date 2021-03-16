using System;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.DomainTests.Students
{
    public class EnrollmentTests
    {
        [Fact]
        public void CourseId_Cant_Be_Empty()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Enrollment(Guid.Empty, Grade.A)
            );
        }
    }
}
