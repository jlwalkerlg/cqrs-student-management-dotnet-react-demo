using System;

namespace StudentManagement.Domain.Courses
{
    public class CourseTitle : ValueObject<CourseTitle>
    {
        public string Title { get; }

        public CourseTitle(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
                throw new InvalidOperationException();

            Title = title;
        }

        protected override bool IsEqual(CourseTitle other)
        {
            return Title == other.Title;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
