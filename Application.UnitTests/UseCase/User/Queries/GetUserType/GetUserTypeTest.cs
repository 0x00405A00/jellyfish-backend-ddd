using AutoMapper;
using Infrastructure.Abstractions;
using Application.CQS.User.Queries.GetUserType;

namespace Application.UnitTests.UseCase.User.Queries.GetUserType
{
    public class GetUserTypeTest
    {
        private static readonly GetUserTypeQuery Query = new GetUserTypeQuery();

        private readonly GetUserTypeQueryHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserTypeRepository _userTypeRepository;

        public GetUserTypeTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userTypeRepository = Substitute.For<IUserTypeRepository>();

            _handler = new GetUserTypeQueryHandler(
                _mapperMock,
                _userTypeRepository);
        }
    }
}
