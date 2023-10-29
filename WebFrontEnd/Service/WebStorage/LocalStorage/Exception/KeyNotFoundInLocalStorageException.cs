using WebFrontEnd.Primitives;

namespace WebFrontEnd.Service.WebStorage.LocalStorage.Exception
{
    public class KeyNotFoundInLocalStorageException : BaseException
    {
        public KeyNotFoundInLocalStorageException(string message) : base(message)
        {
        }
    }
}
