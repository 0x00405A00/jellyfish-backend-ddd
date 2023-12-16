using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Domain.Entities;
using Infrastructure.Abstractions;
using Shared.DataFilter;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUsers
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery,List<UserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Result<List<UserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var searchFilterDto = ColumnSearchAggregateDTOExtension.GetFiltersFromSearchParams<UserDTO, Domain.Entities.User.User>(request.SearchParams);
            if(searchFilterDto.HasErrors)
            {
                return Result<List<UserDTO>>.Failure(searchFilterDto.ErrorsToString);
            }
            var count =await _userRepository.CountMaxAsync();
            if(searchFilterDto.SearchParams.page_size>count && searchFilterDto.SearchParams.page_offset != 1)
            {
                return Result<List<UserDTO>>.Failure("page not found");
            }
            var data = await _userRepository.ListAsyncWithMeta(searchFilterDto);

            var t = _mapper.Map<List<UserDTO>>(data.Data);
            var meta = Meta.Create(data.Meta.TotalItems, data.ColumnSearchAggregateDTO.SearchParams.page_size, data.ColumnSearchAggregateDTO.SearchParams.page_offset);
            return Result<List<UserDTO>>.Success(t, meta);
        }
    }
}
