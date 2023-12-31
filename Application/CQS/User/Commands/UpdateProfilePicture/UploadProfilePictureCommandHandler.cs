using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject;
using Shared.MimePart;

namespace Application.CQS.User.Commands.UpdateProfilePicture
{
    internal sealed class UploadProfilePictureCommandHandler : ICommandHandler<UploadProfilePictureCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMediator mediator;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService _mediaService;

        public UploadProfilePictureCommandHandler(
            IMapper mapper,
            IMediator mediator,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            _mapper = mapper;
            this.mediator = mediator;
            this._userRepository = userRepository;
            this._mediaService = mediaService;
        }

        public async Task<Result<UserDTO>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            //Prüfung des Content in 'UploadProfilePictureCommandValidator'
            if (request.UserId == null)
            {
                return Result<UserDTO>.Failure("user not found");
            }
            var user = await _userRepository.GetAsync(user => user.Id == request.UserId.ToIdentification<UserId>());
            if (user is null)
            {
                return Result<UserDTO>.Failure("user not found");
            }

            var updatedByUser = await _userRepository.GetAsync(x => x.Id == request.UpdatedBy.ToIdentification<UserId>());
            Picture picture = null!;
            try
            {
                var base64ByteArr = Convert.FromBase64String(request.Base64);

                var filePath = _mediaService.CreateProfilePicture(request.UserId, MimeExtension.GetFileExtension(request.MimeType), base64ByteArr, cancellationToken);
                picture = Picture.Parse(filePath, request.MimeType);
                user.UpdatePicture(updatedByUser.Id, picture);
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }

            var mapValue = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
