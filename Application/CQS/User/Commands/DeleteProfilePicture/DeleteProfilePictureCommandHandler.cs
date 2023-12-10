using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.MimePart;

namespace Application.CQS.User.Commands.DeleteProfilePicture
{
    internal sealed class DeleteProfilePictureCommandHandler : ICommandHandler<DeleteProfilePictureCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService _mediaService;

        public DeleteProfilePictureCommandHandler(
            IMediator mediator,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            this.mediator = mediator;
            _userRepository = userRepository;
            _mediaService = mediaService;
        }

        public async Task<Result<bool>> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                return Result<bool>.Failure($"user not found");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user is null)
            {
                return Result<bool>.Failure($"user not found");
            }

            if (!user.HasUserProfilePicture)
            {
                return Result<bool>.Failure($"profile picture is not set");
            }
            var deletedByUser = await _userRepository.GetAsync(x => x.Uuid == request.DeletedBy);

            try
            {

                var filePath = _mediaService.DeleteProfilePicture(request.UserId, MimeExtension.GetFileExtension(user.Picture.FileExtension), cancellationToken);
                user.UpdatePicture(deletedByUser,null);
                _userRepository.Update(user);
            }
            catch (NotValidPhoneNumberException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<bool>.Success(true);
        }


    }
}
