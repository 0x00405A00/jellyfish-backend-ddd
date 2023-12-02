using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class FriendshipRequestDTOExampleProvider : AbstractDTOExampleProvider<FriendshipRequestDTO>
    {
        public override ApiDataTransferObject<FriendshipRequestDTO> GetRequestExample()
        {
            FriendshipRequestDTO dto = new FriendshipRequestDTO
            {
            };

            return ApiDataTransferObject<FriendshipRequestDTO>.Create(dto);
        }
    }
}
