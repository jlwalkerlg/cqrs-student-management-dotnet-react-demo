using System;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class EnrollStudentCommandValidationTests : ValidationTestBase
    {
        private readonly EnrollStudentCommandValidator validator;

        public EnrollStudentCommandValidationTests()
        {
            validator = new EnrollStudentCommandValidator();
        }

        [Fact]
        public void It_Validates_The_Student_Id()
        {
            var command = new EnrollStudentCommand(Guid.Empty, Guid.NewGuid(), "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "StudentId");
        }

        [Fact]
        public void It_Validates_The_Course_Id()
        {
            var command = new EnrollStudentCommand(Guid.NewGuid(), Guid.Empty, "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "CourseId");
        }

        [Fact]
        public void It_Validates_The_Grade()
        {
            var command = new EnrollStudentCommand(Guid.NewGuid(), Guid.NewGuid(), "X");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "Grade");
        }
    }
}
