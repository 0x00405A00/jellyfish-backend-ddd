using Microsoft.AspNetCore.SignalR.Client;

namespace WebFrontEnd.Service.Backend.SignalR.Abstraction
{
    public static class HubConnectionExtension
    {
        public static async Task<bool> ConnectWithRetryAsync(this HubConnection connection, int delayBetweenReinitConnectionInMs, CancellationToken token)
        {
            // Keep trying to until we can start or the token is canceled.

            while (connection.State != HubConnectionState.Connected && !token.IsCancellationRequested)
            {
                try
                {
                    await connection.StartAsync(token);
                }
                catch when (token.IsCancellationRequested)
                {

                }
                catch(Exception ex) 
                {
                    await Task.Delay(delayBetweenReinitConnectionInMs);
                }
            }
            return true;
        }
    }
}
