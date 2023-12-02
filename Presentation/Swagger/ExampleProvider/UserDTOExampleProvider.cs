using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class UserDTOExampleProvider : AbstractDTOExampleProvider<UserDTO>
    {
        public override ApiDataTransferObject<UserDTO> GetRequestExample()
        {
            UserDTO dto = new UserDTO
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@web.de",
            };

            return ApiDataTransferObject<UserDTO>.Create(dto);
        }
    }
}
