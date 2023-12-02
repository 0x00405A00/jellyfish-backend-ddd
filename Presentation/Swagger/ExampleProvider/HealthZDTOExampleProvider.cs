using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class HealthZDTOExampleProvider : AbstractDTOExampleProvider<HealthZDTO>
    {
        public override ApiDataTransferObject<HealthZDTO> GetRequestExample()
        {
            HealthZDTO dto = new HealthZDTO
            {
            };

            return ApiDataTransferObject<HealthZDTO>.Create(dto);
        }
    }
}
