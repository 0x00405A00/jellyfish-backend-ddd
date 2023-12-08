using Application.CQS.User.Queries.GetUserById;
using AutoMapper;
using Infrastructure.Abstractions;

namespace Application.UnitTests.UseCase.User.Queries.GetUserById
{
    public class GetUserByIdTest
    {
        private static readonly GetUserByIdQuery Query = new GetUserByIdQuery(Guid.NewGuid());

        private readonly GetUserByIdQueryHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;

        public GetUserByIdTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();

            _handler = new GetUserByIdQueryHandler(
                _mapperMock,
                _userRepositoryMock);
        }
    }
}
