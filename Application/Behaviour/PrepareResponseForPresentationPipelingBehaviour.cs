using MediatR;
using Shared.ApiDataTransferObject;

namespace Application.Behaviour
{
    public class PrepareResponseForPresentationPipelingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull

    {

        public PrepareResponseForPresentationPipelingBehaviour()
        {

        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!PipeLineBehaviourExtension.IsNoCommand(request))
            {
                return await next();
            }
            var response = await next();
            return response;
        }

    }

}
