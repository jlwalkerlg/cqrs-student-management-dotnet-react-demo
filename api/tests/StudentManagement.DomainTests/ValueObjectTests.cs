using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.DomainTests
{
    public class ValueObjectTests
    {
        [Fact]
        public void Two_Instances_Are_The_Same_If_They_Are_The_Same_Instance()
        {
            var a = new EmailAddress("walker.jlg@gmail.com");
            var b = a;

            Assert.True(a == b);
            Assert.Equal(a, b);
        }

        [Fact]
        public void Two_Instances_Are_The_Same_If_They_Are_Both_Null()
        {
            EmailAddress a = null;
            EmailAddress b = null;

            Assert.True(a == b);
            Assert.Equal(a, b);
        }

        [Fact]
        public void An_Instances_Is_Not_Equal_To_Null()
        {
            EmailAddress a = new EmailAddress("walker.jlg@gmail.com");
            EmailAddress b = null;

            Assert.False(a == b);
            Assert.NotEqual(a, b);
        }
    }
}
