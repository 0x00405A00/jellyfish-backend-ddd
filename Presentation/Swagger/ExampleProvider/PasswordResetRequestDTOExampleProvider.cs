using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class PasswordResetRequestDTOExampleProvider : AbstractDTOExampleProvider<PasswordResetRequestDTO>
    {
        public override ApiDataTransferObject<PasswordResetRequestDTO> GetRequestExample()
        {
            PasswordResetRequestDTO dto = new PasswordResetRequestDTO
            {
            };

            return ApiDataTransferObject<PasswordResetRequestDTO>.Create(dto);
        }
    }
}
