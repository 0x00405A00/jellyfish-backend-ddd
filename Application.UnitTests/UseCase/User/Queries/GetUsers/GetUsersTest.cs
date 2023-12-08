using Application.CQS.User.Queries.GetUsers;
using AutoMapper;
using Infrastructure.Abstractions;

namespace Application.UnitTests.UseCase.User.Queries.GetUsers
{
    public class GetUsersTest
    {
        private static readonly GetUsersQuery Query = new GetUsersQuery(new Shared.DataFilter.Presentation.SearchParams { filters = "[{\"group\":\"and\",\"filters\":[{\"field\":\"deleted_time\",\"value\":null,\"op\":\"eq\"}]},{\"group\":\"and\",\"filters\":[{\"field\":\"email\",\"value\":\"genevieve56@gmail.com\",\"op\":\"eq\"}]}]", order_by = "[{\"field\":\"email\",\"desc\":false}]", page_offset = 1, page_size =5 });

        private readonly GetUsersQueryHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;

        public GetUsersTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();

            _handler = new GetUsersQueryHandler(
                _mapperMock,
                _userRepositoryMock);
        }
    }
}
