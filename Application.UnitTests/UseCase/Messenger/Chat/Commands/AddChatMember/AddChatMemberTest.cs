using Application.CQS.Messenger.Chat.Command.AddChatMember;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.AddChatMember
{
    public class AddChatMemberTest
    {
        private static readonly AddChatMemberCommand Command = new AddChatMemberCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());

        private readonly AddChatMemberCommandHandler _handler;

        private readonly IMediaService _mediaService;
        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _chatRepository;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        public AddChatMemberTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _chatRepository = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();
            _handler = new AddChatMemberCommandHandler(
                _mapperMock,
                _chatRepository,
                _userRepositoryMock,
                _mediaService,
                _unitOfWorkMock);
        }


    }
}
