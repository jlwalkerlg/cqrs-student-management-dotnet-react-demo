using System.Linq;
using System;
using StudentManagement.Domain.Students;
using Xunit;
using StudentManagement.Domain.Courses;

namespace StudentManagement.DomainTests.Students
{
    public partial class StudentTests
    {
        private Student student;
        private Course philosophy;
        private Course physics;
        private Course anthropology;

        public StudentTests()
        {
            student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );

            philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(3)
            );

            physics = new Course(
                new CourseTitle("Physics"),
                new Credits(3)
            );

            anthropology = new Course(
                new CourseTitle("Anthropology"),
                new Credits(3)
            );
        }

        [Fact]
        public void Name_Cant_Be_Null()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Student(null, new EmailAddress("walker.jlg@gmail.com"))
            );
        }

        [Fact]
        public void Email_Cant_Be_Null()
        {
            Assert.Throws<InvalidOperationException>(
                () => new Student(new StudentName("Jordan Walker"), null)
            );
        }

        [Fact]
        public void A_Student_Cant_Enroll_In_More_Than_2_Courses()
        {
            student.Enroll(philosophy, Grade.A);
            student.Enroll(physics, Grade.B);

            Assert.Throws<InvalidOperationException>(() => student.Enroll(anthropology, Grade.C));
        }

        [Fact]
        public void A_Student_Can_Check_If_It_Is_Already_Enrolled_In_A_Course()
        {
            student.Enroll(philosophy, Grade.A);

            Assert.True(student.AlreadyEnrolledIn(philosophy));
        }

        [Fact]
        public void A_Student_Cant_Enroll_In_The_Same_Course_Twice()
        {
            student.Enroll(philosophy, Grade.A);

            Assert.Throws<InvalidOperationException>(() => student.Enroll(philosophy, Grade.C));
        }

        [Fact]
        public void A_Student_Can_Disenroll_From_A_Course()
        {
            student.Enroll(philosophy, Grade.A);
            student.Enroll(physics, Grade.B);

            student.Disenroll(philosophy, "Priorities.");

            Assert.Equal(1, student.Enrollments.Count);
            Assert.DoesNotContain(philosophy.Id, student.Enrollments.Select(e => e.CourseId));
        }

        [Fact]
        public void A_Student_Cant_Disenroll_From_A_Course_They_Arent_Enrolled_On()
        {
            Assert.Throws<InvalidOperationException>(
                () => student.Disenroll(philosophy, "Priorities."));
        }

        [Fact]
        public void A_Student_Cant_Transfer_From_A_Course_They_Arent_Enrolled_On()
        {
            Assert.Throws<InvalidOperationException>(
                () => student.Transfer(physics, anthropology, Grade.A));
        }

        [Fact]
        public void A_Student_Can_Transfer_To_Another_Course()
        {
            student.Enroll(philosophy, Grade.A);
            student.Enroll(physics, Grade.D);

            student.Transfer(physics, anthropology, Grade.B);

            var courseIds = student.Enrollments.Select(e => e.CourseId);
            Assert.DoesNotContain(physics.Id, courseIds);
            Assert.Contains(philosophy.Id, courseIds);
            Assert.Contains(anthropology.Id, courseIds);
        }

        [Fact]
        public void A_Student_Can_Transfer_To_The_Same_Course()
        {
            student.Enroll(philosophy, Grade.A);
            student.Transfer(philosophy, philosophy, Grade.B);

            Assert.Equal(1, student.Enrollments.Count);
            Assert.Equal(philosophy.Id, student.Enrollments.First().CourseId);
            Assert.Equal(Grade.B, student.Enrollments.First().Grade);
        }

        [Fact]
        public void Student_Name_Cant_Be_Empty()
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
    }
}
