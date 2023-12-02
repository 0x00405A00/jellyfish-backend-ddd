using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Presentation.Swagger.ExampleProvider
{
    public class PasswordResetDataTransferModelExampleProvider : AbstractDTOExampleProvider<PasswordResetDataTransferModel>
    {
        public override ApiDataTransferObject<PasswordResetDataTransferModel> GetRequestExample()
        {
            PasswordResetDataTransferModel dto = new PasswordResetDataTransferModel
            {
            };

            return ApiDataTransferObject<PasswordResetDataTransferModel>.Create(dto);
        }
    }
}
