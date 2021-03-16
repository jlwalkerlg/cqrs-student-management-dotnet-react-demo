using FluentValidation;

namespace StudentManagement.Application
{
    public abstract class Validator<TRequest> : AbstractValidator<TRequest>, IValidator<TRequest>
    {
        public new Result Validate(TRequest request)
        {
            var validationResult = base.Validate(request);

            if (validationResult.IsValid) return Result.Ok();

            var error = new ValidationError();

            foreach (var err in validationResult.Errors)
                error.AddError(err.PropertyName, err.ErrorMessage);

            return Result.Fail(error);
        }
    }
}
