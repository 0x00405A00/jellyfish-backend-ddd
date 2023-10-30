using Application.Abstractions.Messaging;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUsers
{
    public sealed class GetUsersQuery : SearchQuery<List<UserDTO>>
    {
        public GetUsersQuery(SearchParams? SearchParams) :base(SearchParams)
        {
                
        }
    }
}
