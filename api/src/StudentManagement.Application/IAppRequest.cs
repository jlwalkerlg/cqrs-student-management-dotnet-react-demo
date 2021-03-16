using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace StudentManagement.Application
{
    public interface IAppRequest<TResponse> : IRequest<Result<TResponse>>
    {
    }

    public interface IAppRequest : IAppRequest<object>
    {
    }

    public interface IAppRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>> where TRequest : IAppRequest<TResponse>
    {
    }

    public abstract class AppRequestHandler<TRequest> : IAppRequestHandler<TRequest, object> where TRequest : IAppRequest
    {
        public abstract Task<Result> Handle(TRequest request, CancellationToken cancellationToken);

        async Task<Result<object>> IRequestHandler<TRequest, Result<object>>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}
