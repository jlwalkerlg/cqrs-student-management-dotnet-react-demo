using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Infrastructure.Data.EF.Repositories;
using StudentManagement.Domain.Courses;
using Xunit;

namespace StudentManagement.InfrastructureTests.Data.EF.Repositories
{
    public class CourseDtoRepositoryTests : RepositoryTestBase
    {
        private readonly CourseDtoRepository repository;

        public CourseDtoRepositoryTests()
        {
            repository = new CourseDtoRepository(context);
        }

        [Fact]
        public async Task It_Gets_A_List_Of_Course_Dtos()
        {
            var philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            var physics = new Course(
                new CourseTitle("Physics"),
                new Credits(2)
            );
            context.Courses.AddRange(philosophy, physics);
            context.SaveChanges();

            var courses = await repository.Get();

            Assert.Equal(2, courses.Count);

            var foundPhilosophy = courses.FirstOrDefault(x => x.Id == philosophy.Id);
            var foundPhysics = courses.FirstOrDefault(x => x.Id == physics.Id);

            Assert.NotNull(foundPhilosophy);
            Assert.NotNull(foundPhysics);

            Assert.Equal(philosophy.Id, foundPhilosophy.Id);
            Assert.Equal(philosophy.Title.Title, foundPhilosophy.Title);
            Assert.Equal(philosophy.Credits.Amount, foundPhilosophy.Credits);
            Assert.Equal(physics.Id, foundPhysics.Id);
            Assert.Equal(physics.Title.Title, foundPhysics.Title);
            Assert.Equal(physics.Credits.Amount, foundPhysics.Credits);
        }
    }
}
