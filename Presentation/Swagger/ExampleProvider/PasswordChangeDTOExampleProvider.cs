using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class PasswordChangeDTOExampleProvider : AbstractDTOExampleProvider<PasswordChangeDTO>
    {
        public override ApiDataTransferObject<PasswordChangeDTO> GetRequestExample()
        {
            PasswordChangeDTO dto = new PasswordChangeDTO
            {
            };

            return ApiDataTransferObject<PasswordChangeDTO>.Create(dto);
        }
    }
}
