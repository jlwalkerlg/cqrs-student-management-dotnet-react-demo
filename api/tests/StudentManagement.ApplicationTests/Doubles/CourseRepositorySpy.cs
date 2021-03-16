using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;
using StudentManagement.Domain.Courses;

namespace StudentManagement.ApplicationTests.Doubles
{
    public class CourseRepositorySpy : ICourseRepository
    {
        public List<Course> Courses { get; } = new List<Course>();

        public Task Add(Course course)
        {
            Courses.Add(course);
            return Task.CompletedTask;
        }

        public async Task<Course> FindById(Guid id)
        {
            await Task.CompletedTask;
            return Courses.FirstOrDefault(x => x.Id == id);
        }
    }
}
