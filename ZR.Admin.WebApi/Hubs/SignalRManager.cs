using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ZR.Admin.WebApi.Hubs
{
    public class SignalRManager:Hub
    {
        private static readonly ConcurrentDictionary<string, HubCallerContext> ConnectedClients = new ConcurrentDictionary<string, HubCallerContext>();
        private static readonly ConcurrentDictionary<string, DateTime> DisconnectedClients = new ConcurrentDictionary<string, DateTime>();
        public static void AddClient(HubCallerContext clientContext)
        {
            ConnectedClients.TryAdd(clientContext.ConnectionId, clientContext);
            DisconnectedClients.TryRemove(clientContext.ConnectionId, out _);
        }

        public static void RemoveClient(HubCallerContext clientContext)
        {
            ConnectedClients.TryRemove(clientContext.ConnectionId, out _);
            DisconnectedClients.TryAdd(clientContext.ConnectionId, DateTime.UtcNow);
        }

        //public static async Task SendToAllAsync(string message)
        //{
        //    foreach (var group in ConnectedClients.Values.GroupBy(x => x.ConnectionId % Environment.ProcessorCount))
        //    {
        //        //await Task.WhenAll(group.Select(x => x?.Clients.All.SendAsync("ReceiveMessage", message)));
        //    }
        //}
    }
}
