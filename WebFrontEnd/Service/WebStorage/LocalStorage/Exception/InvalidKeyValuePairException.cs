using WebFrontEnd.Primitives;

namespace WebFrontEnd.Service.WebStorage.LocalStorage.Exception
{
    public class InvalidKeyValuePairException : BaseException
    {
        public InvalidKeyValuePairException(string message) : base(message)
        {
        }
    }
}
