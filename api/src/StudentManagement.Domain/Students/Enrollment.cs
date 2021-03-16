using System;

namespace StudentManagement.Domain.Students
{
    public class Enrollment : Entity
    {
        public Grade Grade { get; private set; }
        public Guid CourseId { get; }

        public Enrollment(Guid courseId, Grade grade)
        {
            if (courseId == Guid.Empty)
                throw new InvalidOperationException();

            CourseId = courseId;
            Grade = grade;
        }

        public void UpdateGrade(Grade grade)
        {
            Grade = grade;
        }

        private Enrollment() { }
    }
}
