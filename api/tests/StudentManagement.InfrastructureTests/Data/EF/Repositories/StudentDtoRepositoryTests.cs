using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Application.Students;
using StudentManagement.Infrastructure.Data.EF.Repositories;
using StudentManagement.Domain.Courses;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.InfrastructureTests.Data.EF.Repositories
{
    public class StudentDtoRepositoryTests : RepositoryTestBase
    {
        private readonly StudentDtoRepository repository;
        private readonly GetStudentsFilter emptyFilter;

        public StudentDtoRepositoryTests()
        {
            repository = new StudentDtoRepository(context);
            emptyFilter = new GetStudentsFilter();
        }

        [Fact]
        public async Task It_Gets_A_List_Of_Student_Dtos()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            var physics = new Course(
                new CourseTitle("Physics"),
                new Credits(2)
            );
            student.Enroll(philosophy, Grade.A);
            student.Enroll(physics, Grade.B);
            context.Courses.AddRange(philosophy, physics);
            context.Students.Add(student);
            context.SaveChanges();

            var students = await repository.GetStudents(emptyFilter);

            Assert.Single(students);
            Assert.Equal(student.Id, students[0].Id);
            Assert.Equal(student.Name.Name, students[0].Name);
            Assert.Equal(student.Email.Address, students[0].Email);

            var philosophyEnrollment = students[0].Enrollments
                .FirstOrDefault(e => e.Course.Id == philosophy.Id);
            Assert.NotNull(philosophyEnrollment);
            Assert.Equal(philosophy.Id, philosophyEnrollment.Course.Id);
            Assert.Equal(philosophy.Title.Title, philosophyEnrollment.Course.Title);
            Assert.Equal(philosophy.Credits.Amount, philosophyEnrollment.Course.Credits);
            Assert.Equal(Grade.A.ToString(), philosophyEnrollment.Grade);

            var physicsEnrollment = students[0].Enrollments
                .FirstOrDefault(e => e.Course.Id == physics.Id);
            Assert.NotNull(physicsEnrollment);
            Assert.Equal(physics.Id, physicsEnrollment.Course.Id);
            Assert.Equal(physics.Title.Title, physicsEnrollment.Course.Title);
            Assert.Equal(physics.Credits.Amount, physicsEnrollment.Course.Credits);
            Assert.Equal(Grade.B.ToString(), physicsEnrollment.Grade);
        }

        [Fact]
        public async Task Student_Dto_Enrollments_Are_Empty_When_They_Are_Not_Enrolled_In_A_Course()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            var students = await repository.GetStudents(emptyFilter);

            Assert.Empty(students[0].Enrollments);
        }

        [Fact]
        public async Task It_Filters_Students_By_Number_Of_Enrollments()
        {
            var student1 = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var student2 = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("bruno@gmail.com")
            );
            var philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            var physics = new Course(
                new CourseTitle("Physics"),
                new Credits(2)
            );
            student1.Enroll(philosophy, Grade.A);
            student1.Enroll(physics, Grade.B);
            student2.Enroll(philosophy, Grade.A);
            context.Courses.AddRange(philosophy, physics);
            context.Students.AddRange(student1, student2);
            context.SaveChanges();

            var students = await repository.GetStudents(new GetStudentsFilter
            {
                NumberOfCourses = 1,
            });

            Assert.Single(students);
            Assert.Equal(student2.Id, students[0].Id);
        }

        [Fact]
        public async Task It_Filters_Students_By_Course_Enrolled_In()
        {
            var student1 = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var student2 = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("bruno@gmail.com")
            );
            var philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            var physics = new Course(
                new CourseTitle("Physics"),
                new Credits(2)
            );
            student1.Enroll(physics, Grade.B);
            student2.Enroll(philosophy, Grade.A);
            context.Courses.AddRange(philosophy, physics);
            context.Students.AddRange(student1, student2);
            context.SaveChanges();

            var students = await repository.GetStudents(new GetStudentsFilter
            {
                EnrolledIn = philosophy.Id,
            });

            Assert.Single(students);
            Assert.Equal(student2.Id, students[0].Id);
        }
    }
}
