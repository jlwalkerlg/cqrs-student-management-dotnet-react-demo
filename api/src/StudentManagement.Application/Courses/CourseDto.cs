using System;

namespace StudentManagement.Application.Courses
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
