using System;
using StudentManagement.Domain.Courses;
using Xunit;

namespace StudentManagement.DomainTests.Courses
{
    public class CourseTests
    {
        [Fact]
        public void Course_Title_Cant_Be_Null()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Course(null, new Credits(1))
            );
        }

        [Fact]
        public void Credits_Cant_Be_Null()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Course(new CourseTitle("Philosophy"), null)
            );
        }

        [Fact]
        public void Course_Title_Cant_Be_Empty()
        {
            Assert.Throws<InvalidOperationException>(
                () => new CourseTitle("")
            );
            Assert.Throws<InvalidOperationException>(
                () => new CourseTitle(" ")
            );
            Assert.Throws<InvalidOperationException>(
                () => new CourseTitle(null)
            );
        }

        [Fact]
        public void Credits_Cant_Be_Less_Than_1()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Credits(0)
            );
            Assert.Throws<InvalidOperationException>(
                () => new Credits(-1)
            );
        }
    }
}
