using StudentManagement.Application;
using Xunit;

namespace StudentManagement.ApplicationTests
{
    public abstract class ValidationTestBase
    {
        protected void AssertHasValidationError(Result result, string key)
        {
            Assert.True(IsValidationFailure(result));
            Assert.True(HasValidationError(result, key));
        }

        private bool IsValidationFailure(Result result)
        {
            if (!result.IsFailure)
                return false;

            if (result.Error.GetType() != typeof(ValidationError))
                return false;

            return true;
        }

        private bool HasValidationError(Result result, string key)
        {
            return ((ValidationError)result.Error).Has(key);
        }
    }
}
