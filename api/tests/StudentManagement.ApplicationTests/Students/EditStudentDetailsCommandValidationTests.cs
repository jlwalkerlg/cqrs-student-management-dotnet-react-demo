using System;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class EditStudentDetailsCommandValidationTests : ValidationTestBase
    {
        private readonly EditStudentDetailsCommandValidator validator;

        public EditStudentDetailsCommandValidationTests()
        {
            validator = new EditStudentDetailsCommandValidator();
        }

        [Fact]
        public void It_Validates_The_Id()
        {
            var command = new EditStudentDetailsCommand(Guid.Empty, "Jordan Walker", "walker.jlg@gmail.com");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "Id");
        }

        [Fact]
        public void It_Validates_The_Name()
        {
            var command = new EditStudentDetailsCommand(Guid.NewGuid(), "", "walker.jlg@gmail.com");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "Name");
        }

        [Fact]
        public void It_Validates_The_Email()
        {
            EditStudentDetailsCommand command;
            Result<Guid> result;

            command = new EditStudentDetailsCommand(Guid.NewGuid(), "Jordan Walker", "");
            result = validator.Validate(command);
            AssertHasValidationError(result, "Email");

            command = new EditStudentDetailsCommand(Guid.NewGuid(), "Jordan Walker", "not_a_real_email");
            result = validator.Validate(command);
            AssertHasValidationError(result, "Email");
        }
    }
}
