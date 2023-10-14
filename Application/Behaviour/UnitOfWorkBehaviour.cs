using Infrastructure.Abstractions;
using MediatR;

namespace Application.Behaviour
{
    public sealed class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if(IsNoCommand(request))
            {
                return await next();
            }

            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return response;    
        }
        private static bool IsNoCommand(TRequest request)
        {
            return !request.GetType().Name.EndsWith("Command");
        }
    }
}
