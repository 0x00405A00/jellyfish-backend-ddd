using Microsoft.AspNetCore.SignalR.Client;

namespace Shared.Infrastructure.Backend.SignalR.Abstraction
{
    public static class HubConnectionExtension
    {
        /// <summary>
        /// Extension for default <see cref="HubConnection"/> that doesnt handle the reconnect by when no initial connect is happend before
        /// Reconnect is handling when a connection was established and is then interrupted
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="delayBetweenReinitConnectionInMs"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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
                catch (Exception ex)
                {
                    await Task.Delay(delayBetweenReinitConnectionInMs);
                }
            }
            return true;
        }
    }
}
