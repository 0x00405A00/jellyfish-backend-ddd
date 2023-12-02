using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class RegisterUserDTOExampleProvider : AbstractDTOExampleProvider<RegisterUserDTO>
    {
        public override ApiDataTransferObject<RegisterUserDTO> GetRequestExample()
        {
            RegisterUserDTO dto = new RegisterUserDTO
            {
            };

            return ApiDataTransferObject<RegisterUserDTO>.Create(dto);
        }
    }
}
