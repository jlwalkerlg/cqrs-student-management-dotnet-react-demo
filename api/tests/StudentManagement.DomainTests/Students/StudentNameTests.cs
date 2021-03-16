using System;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.DomainTests.Students
{
    public class StudentNameTests
    {
        [Fact]
        public void The_Name_Cant_Be_Empty()
        {
            Assert.Throws<InvalidOperationException>(
                () => new StudentName("")
            );
            Assert.Throws<InvalidOperationException>(
                () => new StudentName(" ")
            );
            Assert.Throws<InvalidOperationException>(
                () => new StudentName(null)
            );
        }

        [Fact]
        public void Two_Instances_Are_The_Same_If_Their_Name_Is_The_Same()
        {
            var a = new StudentName("Jordan Walker");
            var b = new StudentName("Jordan Walker");

            Assert.Equal(a, b);
            Assert.True(a == b);
        }

        [Fact]
        public void Two_Instances_Are_Not_The_Same_If_Their_Name_Is_Not_The_Same()
        {
            var a = new StudentName("Jordan Walker");
            var b = new StudentName("Walker Jordan");

            Assert.NotEqual(a, b);
            Assert.False(a == b);
        }
    }
}
