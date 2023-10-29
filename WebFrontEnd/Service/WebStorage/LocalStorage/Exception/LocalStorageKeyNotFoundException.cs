using System.Runtime.Serialization;

namespace WebFrontEnd.Service.WebStorage.LocalStorage.Exception
{
    [Serializable]
    internal class LocalStorageKeyNotFoundException : System.Exception
    {
        public LocalStorageKeyNotFoundException()
        {
        }

        public LocalStorageKeyNotFoundException(string? message) : base(message)
        {
        }

        public LocalStorageKeyNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected LocalStorageKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}