using Application.Abstractions.Messaging;
using Application.DataTransferObject.Messenger;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Messenger.User.Queries.GetUserById
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUserByIdQuery, MessengerUserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersQueryHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<MessengerUserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetAsync(x => x.Uuid == request.Id);
            if (data == null)
            {
                throw new UserNotFoundException(request.Id);
            }
            var res = Result<MessengerUserDTO>.Success();

            res.Value = _mapper.Map<MessengerUserDTO>(data);
            return res;
        }
    }
}
