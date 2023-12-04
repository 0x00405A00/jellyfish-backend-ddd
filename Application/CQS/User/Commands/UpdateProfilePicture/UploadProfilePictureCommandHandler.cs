using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
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
        private readonly MediaService _mediaService;
        private readonly IUnitOfWork _unitOfWork;

        public UploadProfilePictureCommandHandler(
            IMapper mapper,
            IMediator mediator,
            IUserRepository userRepository,
            MediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.mediator = mediator;
            this._userRepository = userRepository;
            this._mediaService = mediaService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user is null)
                throw new UserNotFoundException(request.UserId);

            var updatedByUser = await _userRepository.GetAsync(x => x.Uuid == request.UpdatedBy);
            Picture picture = null!;
            try
            {
                var base64ByteArr = Convert.FromBase64String(request.Base64);

                var filePath = _mediaService.CreateProfilePicture(request.UserId, MimeExtension.GetFileExtension(request.MimeType), base64ByteArr, cancellationToken);
                picture = Picture.Parse(filePath, request.MimeType);
                user.UpdatePicture(updatedByUser, picture);
                _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }

            var mapValue = _mapper.Map<UserDTO>(user);
            mapValue.Password = null;
            _userRepository.PublishDomainEvents(user,mediator);
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
