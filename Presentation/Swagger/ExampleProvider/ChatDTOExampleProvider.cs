using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Presentation.Swagger.ExampleProvider
{
    public class ChatDTOExampleProvider : AbstractDTOExampleProvider<ChatDTO>
    {
        public override ApiDataTransferObject<ChatDTO> GetRequestExample()
        {
            ChatDTO dto = new ChatDTO
            {
            };

            return ApiDataTransferObject<ChatDTO>.Create(dto);
        }
    }
}
