using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Data.EF.Repositories;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.InfrastructureTests.Data.EF.Repositories
{
    public class StudentRepositoryTests : RepositoryTestBase
    {
        private readonly StudentRepository repo;

        public StudentRepositoryTests() : base()
        {
            repo = new StudentRepository(context);
        }

        [Fact]
        public async Task It_Can_Add_A_Student()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );

            await repo.Add(student);
            context.SaveChanges();

            Assert.Contains(student, context.Students);
        }

        [Fact]
        public async Task It_Can_Find_A_Student_By_Email()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            var found = await repo.FindByEmail("walker.jlg@gmail.com");

            Assert.NotNull(found);
            Assert.Equal(student.Id, found.Id);
        }

        [Fact]
        public async Task It_Can_Find_A_Student_By_Id()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            var found = await repo.FindById(student.Id);

            Assert.NotNull(found);
            Assert.Equal(student.Id, found.Id);
        }

        [Fact]
        public async Task It_Can_Remove_A_Student()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            await repo.Remove(student);
            context.SaveChanges();

            var found = await context.Students.SingleOrDefaultAsync(x => x.Id == student.Id);

            Assert.Null(found);
        }
    }
}
