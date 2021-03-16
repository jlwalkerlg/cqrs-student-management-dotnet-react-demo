using System;
using StudentManagement.Domain.Courses;
using Xunit;

namespace StudentManagement.DomainTests.Courses
{
    public class CreditsTests
    {
        [Fact]
        public void Amount_Must_Be_Greater_Than_Zero()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Credits(0)
            );
            Assert.Throws<InvalidOperationException>(
                () => new Credits(-1)
            );
        }

        [Fact]
        public void Two_Instances_Are_The_Same_If_The_Amount_Is_The_Same()
        {
            var credits1 = new Credits(1);
            var credits2 = new Credits(1);

            Assert.Equal(credits1, credits2);
            Assert.True(credits1 == credits2);
        }

        [Fact]
        public void Two_Instances_Are_Not_The_Same_If_The_Amount_Is_Not_The_Same()
        {
            var credits1 = new Credits(1);
            var credits2 = new Credits(2);

            Assert.NotEqual(credits1, credits2);
            Assert.False(credits1 == credits2);
        }

        [Fact]
        public void Two_Instances_Have_The_Same_Hash_Code_If_The_Amount_Is_The_Same()
        {
            var credits1 = new Credits(1);
            var credits2 = new Credits(1);

            Assert.Equal(credits1.GetHashCode(), credits2.GetHashCode());
        }
    }
}
