using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Presentation.Swagger.ExampleProvider
{
    public class UserFriendshipRequestDTOExampleProvider : AbstractDTOExampleProvider<UserFriendshipRequestDTO>
    {
        public override ApiDataTransferObject<UserFriendshipRequestDTO> GetRequestExample()
        {
            UserFriendshipRequestDTO dto = new UserFriendshipRequestDTO
            {
            };

            return ApiDataTransferObject<UserFriendshipRequestDTO>.Create(dto);
        }
    }
}
