using System;
using StudentManagement.Domain.Students;
using Xunit;
using StudentManagement.Domain;

namespace StudentManagement.DomainTests
{
    public class EntityTests
    {
        private Entity CreateStudent()
        {
            return new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
        }

        [Fact]
        public void A_New_Entity_Has_An_Id()
        {
            var entity = CreateStudent();

            Assert.IsType<Guid>(entity.Id);
            Assert.NotEqual(Guid.Empty, entity.Id);
        }

        [Fact]
        public void Two_Instances_Are_Equal_If_They_Are_Equal_Instance()
        {
            var a = CreateStudent();
            var b = a;

            Assert.True(a == b);
            Assert.Equal(a, b);
        }

        [Fact]
        public void Two_Instances_Are_Not_Equal()
        {
            var a = CreateStudent();
            var b = CreateStudent();

            Assert.False(a == b);
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void An_Instances_Is_Not_Equal_To_Null()
        {
            var a = CreateStudent();
            Student b = null;

            Assert.False(a == b);
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void Two_Null_Instances_Are_Equal()
        {
            Student a = null;
            Student b = null;

            Assert.True(a == b);
            Assert.Equal(a, b);
        }
    }
}
