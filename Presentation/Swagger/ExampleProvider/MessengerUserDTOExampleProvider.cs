using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Presentation.Swagger.ExampleProvider
{
    public class MessengerUserDTOExampleProvider : AbstractDTOExampleProvider<MessengerUserDTO>
    {
        public override ApiDataTransferObject<MessengerUserDTO> GetRequestExample()
        {
            MessengerUserDTO dto = new MessengerUserDTO
            {
            };

            return ApiDataTransferObject<MessengerUserDTO>.Create(dto);
        }
    }
}
