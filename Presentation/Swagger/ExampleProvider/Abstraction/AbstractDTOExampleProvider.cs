using Shared.ApiDataTransferObject;
using Shared.DataTransferObject.Abstraction;

namespace Presentation.Swagger.ExampleProvider.Abstraction
{
    public abstract class AbstractDTOExampleProvider<T> : IDataTransferObjectExampleProvier<T>
        where T : IDataTransferObject
    {
        public virtual ApiDataTransferObject<T> GetRequestExample()
        {
            throw new NotImplementedException();
        }

        public virtual T2 GetResponseExample<T2>()
        {
            throw new NotImplementedException();
        }
    }
}
