using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.Auth.Queries.GetAuthById
{
    public sealed record GetAuthByIdQuery(Guid Id) : IQuery<AuthDTO>;
}
