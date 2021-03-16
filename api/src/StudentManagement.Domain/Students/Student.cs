using System;
using System.Linq;
using System.Collections.Generic;
using StudentManagement.Domain.Courses;

namespace StudentManagement.Domain.Students
{
    public class Student : AggregateRoot
    {
        public Student(StudentName name, EmailAddress email)
        {
            if (name == null)
                throw new InvalidOperationException("Student name can't be null.");
            if (email == null)
                throw new InvalidOperationException("Student email can't be null.");

            Name = name;
            Email = email;
        }

        public StudentName Name { get; set; }
        public EmailAddress Email { get; set; }

        private List<Enrollment> enrollments = new List<Enrollment>();
        public IReadOnlyList<Enrollment> Enrollments => enrollments.ToList();

        public void Enroll(Course course, Grade grade)
        {
            if (enrollments.Count == 2)
                throw new InvalidOperationException();
            if (AlreadyEnrolledIn(course))
                throw new InvalidOperationException();

            enrollments.Add(new Enrollment(course.Id, grade));
        }

        public bool AlreadyEnrolledIn(Course course)
        {
            return enrollments.Any(e => e.CourseId == course.Id);
        }

        public void Disenroll(Course course, string comment)
        {
            var enrollment = enrollments.FirstOrDefault(e => e.CourseId == course.Id);
            if (enrollment == null)
                throw new InvalidOperationException();

            enrollments.Remove(enrollment);
        }

        public void Transfer(Course from, Course to, Grade grade)
        {
            var enrollment = enrollments.FirstOrDefault(e => e.CourseId == from.Id);
            if (enrollment == null)
                throw new InvalidOperationException();

            if (from == to)
            {
                enrollment.UpdateGrade(grade);
            }
            else
            {
                enrollments.Remove(enrollment);
                enrollments.Add(new Enrollment(to.Id, grade));
            }
        }

        private Student() { }
    }
}
