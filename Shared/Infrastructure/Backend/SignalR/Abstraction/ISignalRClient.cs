﻿namespace Shared.Infrastructure.Backend.SignalR.Abstraction
{
    public interface ISignalRClient
    {
        public void Send(string methodName, object[] args, CancellationToken cancellationToken);
        public string GetUserAgent();
    }
}
