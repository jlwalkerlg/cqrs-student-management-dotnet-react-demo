using System;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.DomainTests.Students
{
    public class EmailAddressTests
    {
        [Fact]
        public void The_Address_Cant_Be_Empty()
        {
            Assert.Throws<InvalidOperationException>(
                () => new EmailAddress("")
            );
            Assert.Throws<InvalidOperationException>(
                () => new EmailAddress(" ")
            );
            Assert.Throws<InvalidOperationException>(
                () => new EmailAddress(null)
            );
        }

        [Fact]
        public void Two_Instances_Are_The_Same_If_The_Address_Is_The_Same()
        {
            var email1 = new EmailAddress("walker.jlg@gmail.com");
            var email2 = new EmailAddress("walker.jlg@gmail.com");

            Assert.Equal(email1, email2);
            Assert.Equal(email1, email2);
            Assert.True(email1 == email2);
        }

        [Fact]
        public void Two_Instances_Are_Not_The_Same_If_The_Address_Is_Not_The_Same()
        {
            var email1 = new EmailAddress("walker.jlg@gmail.com");
            var email2 = new EmailAddress("jlg.walker@gmail.com");

            Assert.NotEqual(email1, email2);
            Assert.False(email1 == email2);
        }

        [Fact]
        public void Two_Instances_Have_The_Same_Hash_Code_If_The_Address_Is_The_Same()
        {
            var email1 = new EmailAddress("walker.jlg@gmail.com");
            var email2 = new EmailAddress("walker.jlg@gmail.com");

            Assert.Equal(email1.GetHashCode(), email2.GetHashCode());
        }
    }
}
