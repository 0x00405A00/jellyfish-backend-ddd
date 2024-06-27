using DotNet.Testcontainers.Containers;
using System.Net.Sockets;
using System.Net;
using Testcontainers.PostgreSql;

namespace Integration.Tests
{
    public static class TestcontainerWorkaround
    {
        public static Task<bool> WaitForPort(this PostgreSqlContainer container, TimeSpan? maxWait = null)
        {
            return WaitForPort(container, PostgreSqlBuilder.PostgreSqlPort, maxWait ?? TimeSpan.FromSeconds(10));
        }

        public static async Task<bool> WaitForPort(this DockerContainer container, int unmappedPort, TimeSpan maxWait)
        {
            var ips = await Dns.GetHostAddressesAsync(container.Hostname);
            if (ips.Length == 0)
            {
                throw new ArgumentException($"Expected minimum 1 IP to resolve from '{container.Hostname}', but got {ips.Length}");
            }

            int portNumber = container.GetMappedPublicPort(unmappedPort);

            CancellationTokenSource ts = new();
            ts.CancelAfter(maxWait);

            using var tcpClient = new TcpClient();

            while (!ts.IsCancellationRequested)
            {
                try
                {
                    await tcpClient.ConnectAsync(ips[0], portNumber, ts.Token);
                    return true;
                }
                catch (SocketException) { }
                await Task.Delay(500, ts.Token);
            }

            return false;
        }
    }
}
