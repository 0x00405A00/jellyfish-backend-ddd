using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Presentation.Swagger.ExampleProvider
{
    public class RoleDTOExampleProvider : AbstractDTOExampleProvider<RoleDTO>
    {
        public override ApiDataTransferObject<RoleDTO> GetRequestExample()
        {
            RoleDTO dto = new RoleDTO
            {
            };

            return ApiDataTransferObject<RoleDTO>.Create(dto);
        }
    }
}
