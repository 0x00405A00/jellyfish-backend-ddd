using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Presentation.Swagger.ExampleProvider
{
    public class MessageDTOExampleProvider : AbstractDTOExampleProvider<MessageDTO>
    {
        public override ApiDataTransferObject<MessageDTO> GetRequestExample()
        {
            MessageDTO dto = new MessageDTO
            {
            };

            return ApiDataTransferObject<MessageDTO>.Create(dto);
        }
    }
}
