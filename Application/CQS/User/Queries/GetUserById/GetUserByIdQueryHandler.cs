using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUserById
{
    internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetAsync(x => x.Uuid == request.Id && x.DeletedTime == null);
            if (data == null)
            {
                throw new UserNotFoundException(request.Id);
            }

            var result  = _mapper.Map<UserDTO>(data);
            return Result<UserDTO>.Success(result);
        }
    }
}
