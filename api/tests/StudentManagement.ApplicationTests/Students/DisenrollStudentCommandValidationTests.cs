using System;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class DisenrollStudentCommandValidationTests : ValidationTestBase
    {
        private readonly DisenrollStudentCommandValidator validator;

        public DisenrollStudentCommandValidationTests()
        {
            validator = new DisenrollStudentCommandValidator();
        }

        [Fact]
        public void It_Validates_The_Student_Id()
        {
            var command = new DisenrollStudentCommand(Guid.Empty, Guid.NewGuid(), "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "StudentId");
        }

        [Fact]
        public void It_Validates_The_Course_Id()
        {
            var command = new DisenrollStudentCommand(Guid.NewGuid(), Guid.Empty, "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "CourseId");
        }

        [Fact]
        public void It_Validates_The_Comment()
        {
            DisenrollStudentCommand command;
            Result result;

            command = new DisenrollStudentCommand(Guid.NewGuid(), Guid.NewGuid(), null);
            result = validator.Validate(command);
            AssertHasValidationError(result, "Comment");

            command = new DisenrollStudentCommand(Guid.NewGuid(), Guid.NewGuid(), string.Empty);
            result = validator.Validate(command);
            AssertHasValidationError(result, "Comment");

            command = new DisenrollStudentCommand(Guid.NewGuid(), Guid.NewGuid(), " ");
            result = validator.Validate(command);
            AssertHasValidationError(result, "Comment");
        }
    }
}
