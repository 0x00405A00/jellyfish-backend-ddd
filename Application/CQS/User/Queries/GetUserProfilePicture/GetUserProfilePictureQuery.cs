using Application.Abstractions.Messaging;
using Domain.ValueObjects;

namespace Application.CQS.User.Queries.GetUserProfilePicture
{
    public sealed record GetUserProfilePictureQuery(Guid UserId) : IQuery<MediaContent>;
}

