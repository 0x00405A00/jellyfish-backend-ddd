using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Auth.Queries.GetAuthById
{
    public sealed record GetAuthByIdQuery(Guid Id) : IQuery<AuthDTO>;
}
