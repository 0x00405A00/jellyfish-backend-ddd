using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUserById
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUserByIdQuery, UserDTO>
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
        public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetAsync(x => x.Uuid == request.Id);
            if (data == null)
            {
                throw new UserNotFoundException(request.Id);
            }
            var res = Result<UserDTO>.Success();

            res.Value = _mapper.Map<UserDTO>(data);
            return res;
        }
    }
}
