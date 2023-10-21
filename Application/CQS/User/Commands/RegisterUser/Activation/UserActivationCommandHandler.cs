using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    internal sealed class UserActivationCommandHandler : ICommandHandler<UserActivationCommand, UserDTO>
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserActivationCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(UserActivationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(user => user.ActivationToken == request.Base64Token);
            if (user == null)
                throw new UserNotFoundException(request.Base64Token);

            try
            {
                user.Activate(request.ActivationCode, request.Base64Token);
            }
            catch(InvalidActivationTry ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }

            _userRepository.Update(user);
            user.DomainEvents.ToList().ForEach(e => {
                mediator.Publish(e);
            });

            var result = Result<UserDTO>.Success();
            var mapValue = _mapper.Map<UserDTO>(user);
            mapValue.Password = null;
            result.Value = mapValue;
            return result;
        }
    }
}
