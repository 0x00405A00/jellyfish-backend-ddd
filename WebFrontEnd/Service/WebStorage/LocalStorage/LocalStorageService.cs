using Microsoft.JSInterop;
using Shared.DataTransferObject;
using System.Text.Json;
using WebFrontEnd.Const;
using WebFrontEnd.Service.WebStorage.LocalStorage.Exception;

namespace WebFrontEnd.Service.WebStorage.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        public IJSRuntime JsRuntime { get; }


        public LocalStorageService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task SetItem(string key, string value)
        {
            if(String.IsNullOrEmpty(key))
            {
                throw new InvalidKeyValuePairException("key is null");
            }
            await JsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task<string> GetItem(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidKeyValuePairException("key is null");
            }
            try
            {
                return await JsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            }
            catch (System.Exception ex)
            {
                throw new LocalStorageKeyNotFoundException(ex.Message);
            }
        }

        public async Task RemoveItem(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidKeyValuePairException("key is null");
            }
            try
            {
                await JsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch(System.Exception ex)
            {
                throw new LocalStorageKeyNotFoundException(ex.Message);
            }
        }

        public async Task<T> GetDeserializedJsonItemFromKey<T>(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidKeyValuePairException("key is null");
            }
            try
            {

                string json = await GetItem(key);
                if (string.IsNullOrWhiteSpace(json))
                    return default;
                T value = JsonSerializer.Deserialize<T>(json);

                return value;
            }
            catch(System.Exception ex)
            {
                throw;
                return default;
            }
        }

        public async Task SetDeserializedJsonItemFromKey<T>(string key, T value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidKeyValuePairException("key is null");
            }
            string str = JsonSerializer.Serialize(value);
            await JsRuntime.InvokeVoidAsync("localStorage.setItem", key, str);
        }

        public async Task<bool> CheckIfValidTokenExists()
        {
            try
            {
                var value = await GetDeserializedJsonItemFromKey<AuthDTO>(WebAppAuthorizationConst.BrowserLocalStorageItemKey.Authorization);
                if(value!=null)
                {
                    return !value.IsTokenExpired;
                }
                return false;
            }
            catch(System.Exception ex)
            {
                return false;
            }
        }
    }
}
