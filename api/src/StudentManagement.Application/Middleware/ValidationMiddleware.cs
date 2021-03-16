using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace StudentManagement.Application.Middleware
{
    public class ValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    {
        private readonly IValidator<TRequest> validator;

        public ValidationMiddleware(IValidator<TRequest> validator)
        {
            this.validator = validator;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            var validationResult = validator.Validate(request);
            if (validationResult.IsFailure)
                return Result.Fail<TResponse>(validationResult.Error);

            return await next();
        }
    }
}
