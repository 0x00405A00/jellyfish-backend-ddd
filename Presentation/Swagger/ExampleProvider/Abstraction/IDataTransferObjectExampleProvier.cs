using Shared.ApiDataTransferObject;
using Shared.DataTransferObject.Abstraction;

namespace Presentation.Swagger.ExampleProvider.Abstraction
{
    public interface IDataTransferObjectExampleProvier<T>
        where T : IDataTransferObject
    {
        public ApiDataTransferObject<T> GetRequestExample();
        public T2 GetResponseExample<T2>();
    }
}
