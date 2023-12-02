using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class UserActivationDataTransferModelExampleProvider : AbstractDTOExampleProvider<UserActivationDataTransferModel>
    {
        public override ApiDataTransferObject<UserActivationDataTransferModel> GetRequestExample()
        {
            UserActivationDataTransferModel dto = new UserActivationDataTransferModel
            {
            };

            return ApiDataTransferObject<UserActivationDataTransferModel>.Create(dto);
        }
    }
}
