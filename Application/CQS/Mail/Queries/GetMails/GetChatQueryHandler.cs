using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.Mail.Queries.GetMails
{
    internal sealed class GetMailsQueryHandler : IQueryHandler<GetMailsQuery, List<MailOutboxDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IMailoutboxRepository mailoutboxRepository;
        public GetMailsQueryHandler(
            IMapper mapper,
            IMailoutboxRepository mailoutboxRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.mailoutboxRepository = mailoutboxRepository;
        }
        public async Task<Result<List<MailOutboxDTO>>> Handle(GetMailsQuery request, CancellationToken cancellationToken)
        {
            var data = await mailoutboxRepository.ListAsync();
            
            var result = _mapper.Map<List<MailOutboxDTO>>(data);
            return Result<List<MailOutboxDTO>>.Success(result);
        }
    }
}
