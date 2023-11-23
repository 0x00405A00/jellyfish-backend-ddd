using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using Shared.MimePart;

namespace Application.CQS.User.Commands.DeleteProfilePicture
{
    internal sealed class DeleteProfilePictureCommandHandler : ICommandHandler<DeleteProfilePictureCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly MediaService _mediaService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProfilePictureCommandHandler(
            IUserRepository userRepository,
            MediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mediaService = mediaService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsyncDbEntity(user => user.Uuid == request.UserId);
            if (user is null)
                throw new UserNotFoundException(request.UserId);

            if(user.ProfilePicturePath is null)
            {
                throw new FileNotFoundException($"profile picture is not set");
            }
            try
            {



                var filePath = _mediaService.DeleteProfilePicture(request.UserId, cancellationToken);
                user.ProfilePicturePath = null;
                user.ProfilePictureFileExt = null;
                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (NotValidPhoneNumberException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            return Result<bool>.Success(true);
        }
    }
}
