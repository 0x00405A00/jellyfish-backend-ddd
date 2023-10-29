using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Mail.Queries.GetMails
{
    public sealed record GetMailsQuery() : IQuery<List<MailOutboxDTO>>;
}
