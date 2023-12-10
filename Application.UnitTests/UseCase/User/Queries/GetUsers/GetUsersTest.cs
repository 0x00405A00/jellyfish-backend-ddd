using Application.CQS.User.Queries.GetUsers;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Repository.Primitives;
using Shared.DataFilter;
using Shared.DataFilter.Infrastructure;
using Shared.DataTransferObject;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Queries.GetUsers
{
    public class GetUsersTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        //filter with conditional groups
        private static readonly GetUsersQuery ValidQueryWithConditionalGroups = new GetUsersQuery(new Shared.DataFilter.Presentation.SearchParams { filters = "[{\"group\":\"and\",\"filters\":[{\"field\":\"deleted_time\",\"value\":null,\"op\":\"eq\"}]},{\"group\":\"and\",\"filters\":[{\"field\":\"email\",\"value\":\"johndoe@example.com\",\"op\":\"eq\"}]}]", order_by = "[{\"field\":\"email\",\"desc\":false}]", page_offset = 1, page_size = 5 });

        //simple filter without concatination of contions like or,and,xor
        private static readonly Shared.DataFilter.Presentation.SearchParams SearchParamWithFilterOnly = new Shared.DataFilter.Presentation.SearchParams { filters = "[{\"field\":\"email\",\"value\":\"johndoe@example.com\"}]", page_offset = 1, page_size = 5 };
        private static readonly Shared.DataFilter.Presentation.SearchParams InvalidSearchParamWithFilterOnly = new Shared.DataFilter.Presentation.SearchParams { filters = "[{\"field\":\"fielddoenstexists\",\"value\":\"johndoe@example.com\"}]", page_offset = 1, page_size = 5 };
        private static readonly Shared.DataFilter.Presentation.SearchParams InvalidSearchParamWithInvalidSorting = new Shared.DataFilter.Presentation.SearchParams { order_by = "[{\"field\":\"fielddoesnotexists\",\"desc\":false}]" };
        
        private static readonly GetUsersQuery ValidQueryWithoutConditionalGroups = new GetUsersQuery(SearchParamWithFilterOnly);
        private static readonly GetUsersQuery InvalidQueryWithoutConditionalGroups = new GetUsersQuery(InvalidSearchParamWithFilterOnly);
        private static readonly GetUsersQuery InvalidQueryWithoutInvalidSorting = new GetUsersQuery(InvalidSearchParamWithInvalidSorting);

        private readonly GetUsersQueryHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;

        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        


        public GetUsersTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();

            _handler = new GetUsersQueryHandler(
                _mapperMock,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_GetUsersQueryWithConditionalGroups_Success()
        {
            //Arrange
            var meta = new Infrastructure.Repository.Primitives.Meta(); 
            var searchFilterDto = ColumnSearchAggregateDTOExtension.GetFiltersFromSearchParams<UserDTO, Infrastructure.DatabaseEntity.User>(ValidQueryWithConditionalGroups.SearchParams);
            var listAsyncWithMetaReturns = new List<Domain.Entities.User.User>() { UserInstance };
            _userRepositoryMock.ListAsyncWithMeta(Arg.Any<ColumnSearchAggregateDTO>()).Returns(new RepositoryResponse<ICollection<Domain.Entities.User.User>>(listAsyncWithMetaReturns, ref meta, searchFilterDto)
            {

            }); 
            _userRepositoryMock.CountMaxAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(1);

            // Act
            var result = await _handler.Handle(ValidQueryWithConditionalGroups, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_GetUsersQueryWithFilterOnly_Success()
        {
            //Arrange
            var meta = new Infrastructure.Repository.Primitives.Meta();
            var searchFilterDto = ColumnSearchAggregateDTOExtension.GetFiltersFromSearchParams<UserDTO, Infrastructure.DatabaseEntity.User>(ValidQueryWithConditionalGroups.SearchParams);
            var listAsyncWithMetaReturns = new List<Domain.Entities.User.User>() { UserInstance };
            _userRepositoryMock.ListAsyncWithMeta(Arg.Any<ColumnSearchAggregateDTO>()).Returns(new RepositoryResponse<ICollection<Domain.Entities.User.User>>(listAsyncWithMetaReturns, ref meta, searchFilterDto)
            {

            });
            _userRepositoryMock.CountMaxAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(1);

            // Act
            var result = await _handler.Handle(ValidQueryWithoutConditionalGroups, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_GetUsersQueryWithFilterOnly_Failure()
        {
            //Arrange
            var meta = new Infrastructure.Repository.Primitives.Meta();
            var searchFilterDto = ColumnSearchAggregateDTOExtension.GetFiltersFromSearchParams<UserDTO, Infrastructure.DatabaseEntity.User>(InvalidQueryWithoutConditionalGroups.SearchParams);
            var listAsyncWithMetaReturns = new List<Domain.Entities.User.User>() { };
            _userRepositoryMock.ListAsyncWithMeta(Arg.Any<ColumnSearchAggregateDTO>()).Returns(new RepositoryResponse<ICollection<Domain.Entities.User.User>>(listAsyncWithMetaReturns, ref meta, searchFilterDto)
            {

            });
            _userRepositoryMock.CountMaxAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(1);

            // Act
            var result = await _handler.Handle(InvalidQueryWithoutConditionalGroups, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_GetUsersQueryWithInvalidSorting_Failure()
        {
            //Arrange
            var meta = new Infrastructure.Repository.Primitives.Meta();
            var searchFilterDto = ColumnSearchAggregateDTOExtension.GetFiltersFromSearchParams<UserDTO, Infrastructure.DatabaseEntity.User>(InvalidQueryWithoutConditionalGroups.SearchParams);
            var listAsyncWithMetaReturns = new List<Domain.Entities.User.User>() { };
            _userRepositoryMock.ListAsyncWithMeta(Arg.Any<ColumnSearchAggregateDTO>()).Returns(new RepositoryResponse<ICollection<Domain.Entities.User.User>>(listAsyncWithMetaReturns, ref meta, searchFilterDto)
            {

            });
            _userRepositoryMock.CountMaxAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(1);

            // Act
            var result = await _handler.Handle(InvalidQueryWithoutInvalidSorting, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
