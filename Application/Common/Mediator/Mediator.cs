using Application.Common.Mediator.Interfaces;

namespace Application.Common.Mediator
{
    public class Mediator(IServiceProvider serviceProvider) : IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            var handler = serviceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod("Handle")!;
            Task<TResponse> handlerDelegate() =>
                (Task<TResponse>)method.Invoke(handler, [request, cancellationToken])!;

            var behaviors = serviceProvider
                .GetServices(typeof(IPipelineBehavior<,>)
                    .MakeGenericType(requestType, typeof(TResponse)))
                .Cast<dynamic>()
                .ToList();

            RequestHandlerDelegate<TResponse> pipeline = handlerDelegate;

            foreach (var behavior in Enumerable.Reverse(behaviors))
            {
                var next = pipeline;
                pipeline = () => behavior.Handle((dynamic)request, next, cancellationToken);
            }

            return pipeline();
        }
    }
}