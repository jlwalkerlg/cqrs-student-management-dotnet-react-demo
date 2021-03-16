using System;

namespace StudentManagement.Domain.Courses
{
    public class Course : AggregateRoot
    {
        public Course(CourseTitle title, Credits credits)
        {
            if (title == null)
                throw new InvalidOperationException();
            if (credits == null)
                throw new InvalidOperationException();

            Title = title;
            Credits = credits;
        }

        public CourseTitle Title { get; set; }
        public Credits Credits { get; set; }

        private Course() { }
    }
}
