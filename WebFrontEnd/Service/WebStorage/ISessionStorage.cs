using WebFrontEnd.DataTransferObject.SessionStorage;

namespace WebFrontEnd.Service.WebStorage
{
    public interface ISessionStorage
    {
        Task<SessionDTO> GetSession(CancellationToken cancellationToken);
        void SetSession(SessionDTO sessionDTO,CancellationToken cancellationToken);
    }
}
