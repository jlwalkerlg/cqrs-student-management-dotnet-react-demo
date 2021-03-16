using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace StudentManagement.Application.Middleware
{
    public class LoggingMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            System.Console.WriteLine($"Handling {typeof(TRequest).Name}");
            var response = await next();
            System.Console.WriteLine($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }
}
