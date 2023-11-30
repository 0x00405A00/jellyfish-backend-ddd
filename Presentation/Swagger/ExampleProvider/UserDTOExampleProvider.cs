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

            automatische generierung von testeinträgen über reflection (postman random funktionen like), generierung von emails etc. mit berücksichtigung welche felder mandantoy sind oder nicht (über optional operator ? oder DataAnnotation Attributes)
            return ApiDataTransferObject<UserDTO>.Create(dto);
        }
    }
}
