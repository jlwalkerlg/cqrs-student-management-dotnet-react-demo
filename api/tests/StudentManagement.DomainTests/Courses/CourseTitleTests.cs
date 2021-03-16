using System;
using StudentManagement.Domain.Courses;
using Xunit;

namespace StudentManagement.DomainTests.Courses
{
    public class CourseTitleTests
    {
        [Fact]
        public void It_Cant_Be_Empty()
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
        public void Two_Instances_Are_The_Same_If_The_Title_Is_The_Same()
        {
            var course1 = new CourseTitle("Philosophy");
            var course2 = new CourseTitle("Philosophy");

            Assert.Equal(course1, course2);
            Assert.True(course1 == course2);
        }

        [Fact]
        public void Two_Instances_Are_Not_The_Same_If_The_Title_Is_Not_The_Same()
        {
            var course1 = new CourseTitle("Philosophy");
            var course2 = new CourseTitle("Physics");

            Assert.NotEqual(course1, course2);
            Assert.False(course1 == course2);
        }

        [Fact]
        public void Two_Instances_Have_The_Same_Hash_Code_If_The_Title_Is_The_Same()
        {
            var course1 = new CourseTitle("Philosophy");
            var course2 = new CourseTitle("Philosophy");

            Assert.Equal(course1.GetHashCode(), course2.GetHashCode());
        }
    }
}
