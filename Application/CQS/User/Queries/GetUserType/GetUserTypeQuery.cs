using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUserType
{
    public sealed record GetUserTypeQuery() : IQuery<List<UserTypeDTO>>;
}
