using System;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class GetStudentsQueryValidationTests : ValidationTestBase
    {
        private readonly GetStudentsQueryValidator validator;

        public GetStudentsQueryValidationTests()
        {
            validator = new GetStudentsQueryValidator();
        }

        [Fact]
        public void It_Passes_For_Null_Values()
        {
            var query = new GetStudentsQuery();
            var result = validator.Validate(query);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void It_Validates_The_Number_Of_Courses()
        {
            GetStudentsQuery query;
            Result result;

            query = new GetStudentsQuery(numberOfCourses: -1);
            result = validator.Validate(query);
            AssertHasValidationError(result, "NumberOfCourses");

            query = new GetStudentsQuery(numberOfCourses: 3);
            result = validator.Validate(query);
            AssertHasValidationError(result, "NumberOfCourses");

            query = new GetStudentsQuery(numberOfCourses: 2);
            result = validator.Validate(query);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void It_Validates_The_Name()
        {
            GetStudentsQuery query;
            Result result;


            query = new GetStudentsQuery(enrolledIn: Guid.Empty);
            // System.Console.WriteLine($"Enrolled in has value: {query.EnrolledIn.HasValue}");

            result = validator.Validate(query);
            AssertHasValidationError(result, "EnrolledIn");

            query = new GetStudentsQuery(enrolledIn: Guid.NewGuid());
            System.Console.WriteLine($"Enrolled in has value: {query.EnrolledIn.HasValue}");

            result = validator.Validate(query);
            Assert.True(result.IsSuccess);
        }
    }
}
