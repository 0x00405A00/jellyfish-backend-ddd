using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Domain.Entities;
using Infrastructure.Abstractions;
using Shared.DataFilter;
using Shared.DataTransferObject;
using System.Reflection;
using System.Text.Json.Serialization;

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
            var searchFilterDto = ColumnSearchAggregateDTOExtension.GetFiltersFromSearchParams<UserDTO, Domain.Entities.Users.User>(request.SearchParams);
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

            var t3 = await _userRepository.TestA();


            var properties = typeof(UserDTO).GetProperties()
                    .Where(x => x.CanWrite && (x.PropertyType == typeof(string) || (x.PropertyType == typeof(Guid) || x.PropertyType == typeof(Guid?)) || (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?))))
                    .Select(x => new { attr = x.GetCustomAttribute<JsonPropertyNameAttribute>(), propertyType = x.PropertyType })
                    .ToList();


            return Result<List<UserDTO>>.Success(t, meta);
        }
    }
}
