using System.Threading.Tasks;
using StudentManagement.Infrastructure.Data.EF.Repositories;
using StudentManagement.Domain.Courses;
using Xunit;

namespace StudentManagement.InfrastructureTests.Data.EF.Repositories
{
    public class CourseRepositoryTests : RepositoryTestBase
    {
        private readonly CourseRepository repo;

        public CourseRepositoryTests() : base()
        {
            repo = new CourseRepository(context);
        }

        [Fact]
        public async Task It_Can_Find_A_Course_By_Id()
        {
            var course = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            context.Courses.Add(course);
            context.SaveChanges();

            var found = await repo.FindById(course.Id);

            Assert.NotNull(found);
            Assert.Equal(course.Id, found.Id);
        }
    }
}
