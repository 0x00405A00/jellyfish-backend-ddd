using System.Runtime.CompilerServices;

namespace Shared.Infrastructure.Backend
{
    public interface ILocalStorageService
    {
        Task<string> GetItem(string key);
        Task<T> GetDeserializedJsonItemFromKey<T>(string key, [CallerMemberName] object caller = null);
        Task RemoveItem(string key);
        Task SetItem(string key, string value);
        Task SetDeserializedJsonItemFromKey<T>(string key, T value);
        Task<bool> CheckIfValidTokenExists();
    }
}