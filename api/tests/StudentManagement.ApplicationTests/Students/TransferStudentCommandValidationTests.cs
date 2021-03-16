using System;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class TransferStudentCommandValidationTests : ValidationTestBase
    {
        private readonly TransferStudentCommandValidator validator;

        public TransferStudentCommandValidationTests()
        {
            validator = new TransferStudentCommandValidator();
        }

        [Fact]
        public void It_Validates_The_Student_Id()
        {
            var command = new TransferStudentCommand(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "StudentId");
        }

        [Fact]
        public void It_Validates_The_From_Course_Id()
        {
            var command = new TransferStudentCommand(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "FromCourseId");
        }

        [Fact]
        public void It_Validates_The_To_Course_Id()
        {
            var command = new TransferStudentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "A");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "ToCourseId");
        }

        [Fact]
        public void It_Validates_The_Grade()
        {
            var command = new TransferStudentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "X");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "Grade");
        }
    }
}
