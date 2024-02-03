using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.MimePart;

namespace Application.CQS.User.Queries.GetUserProfilePicture
{
    internal sealed class GetUserProfilePictureQueryHandler : IQueryHandler<GetUserProfilePictureQuery, MediaContent>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public GetUserProfilePictureQueryHandler(
            IMapper mapper,
            IUserRepository userTypeRepository)
        {
            _mapper = mapper;
            _userRepository = userTypeRepository;
        }
        public async Task<Result<MediaContent>> Handle(GetUserProfilePictureQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x=>x.Id == request.UserId.ToIdentification<UserId>());
            if(user is null)
            {
                return Result<MediaContent>.Failure("user doesnt exists");
            }
            if(!user.HasUserProfilePicture)
            {
                return Result<MediaContent>.Failure("no media");
            }
            string extension = MimeExtension.GetFileExtension(user.Picture.FileExtension);
            string fileName = $"{user.Id.Id}.{extension}";
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot")+ user.Picture.FilePath;
            if (!System.IO.File.Exists(path))
            {
                return Result<MediaContent>.Failure("no media");
            }
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);

            MediaContent mediaContent = MediaContent.Parse(imageBytes, user.Picture.FileExtension);
            return Result<MediaContent>.Success(mediaContent);
        }
    }
}
