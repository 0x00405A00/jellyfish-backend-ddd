using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using Shared.MimePart;

namespace Application.CQS.User.Commands.UpdateProfilePicture
{
    internal sealed class UploadProfilePictureCommandHandler : ICommandHandler<UploadProfilePictureCommand,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly MediaService _mediaService;
        private readonly IUnitOfWork _unitOfWork;

        public UploadProfilePictureCommandHandler(
            IUserRepository userRepository,
            MediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._mediaService = mediaService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsyncDbEntity(user => user.Uuid == request.UserId);
            if (user is null)
                throw new UserNotFoundException(request.UserId);

            if(!MimeExtension.IsValidMimeTypeForMediaContent(request.MimeType))
            {
                return Result<bool>.Failure("not supported mime-type");
            }
            Picture picture = null!;
            try
            {


                var base64ByteArr = Convert.FromBase64String(request.Base64);

                var filePath = _mediaService.CreateProfilePicture(request.UserId, base64ByteArr, cancellationToken);
                user.ProfilePicturePath = filePath;
                user.ProfilePictureFileExt = request.MimeType;
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
