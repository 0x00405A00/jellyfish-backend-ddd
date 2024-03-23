using Domain.Primitives.Ids;
using SharpCompress;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Infrastructure.SignalR
{
    public class MessengerHubExtension
    {
        private static Dictionary<string, UserId> _connections = new Dictionary<string, UserId>();
        public static ImmutableDictionary<string, UserId> Connections => _connections.ToImmutableDictionary();
        public static bool HasConnectedClients => _connections.Count > 0;   

        public static void AddConnection(string connectionId,UserId userId) => _connections.Add(connectionId,userId);
        public static void RemoveConnection(string connectionId) => _connections.Remove(connectionId);
        public static string GetConnectionIdByUserId(UserId userId)
        {
            foreach (var connectionId in _connections)
            {
                if(connectionId.Value ==  userId) return connectionId.Key;
            }
            return null;
        }
    }
}
