using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;
using WebFrontEnd.Const;
using WebFrontEnd.DataTransferObject.SessionStorage;

namespace WebFrontEnd.Service.WebStorage
{
    public class SessionStorage: ISessionStorage
    {
        private readonly ILogger<SessionStorage> logger;
        private readonly ILocalStorageService localStorageService;

        public SessionStorage(
            ILogger<SessionStorage> logger,
            ILocalStorageService localStorageService)
        {
            this.logger = logger;
            this.localStorageService = localStorageService;
        }

        public async Task<SessionDTO> GetSession(CancellationToken cancellationToken)
        {
            SessionDTO sessionStorage = new SessionDTO();
            try
            {
                sessionStorage = await localStorageService.GetDeserializedJsonItemFromKey<SessionDTO>(WebAppConst.BrowserLocalStorageItemKey.SessionStorage);
            }
            catch (Exception ex)
            {

            }
            return sessionStorage??new SessionDTO();
        }
        public async void SetSession(SessionDTO sessionStorage, CancellationToken cancellationToken)
        {
            await localStorageService.SetDeserializedJsonItemFromKey(WebAppConst.BrowserLocalStorageItemKey.SessionStorage, sessionStorage);
        }
    }
}
