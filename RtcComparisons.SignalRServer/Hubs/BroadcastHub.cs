using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RtcComparisons.SignalRServer.Hubs
{
	public class BroadcastHub : Hub
	{
		public BroadcastHub()
		{
			
		}

		public override Task OnConnectedAsync()
		{
			Console.WriteLine($"[{this.Context.ConnectionId}] connected.");
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			Console.WriteLine($"[{this.Context.ConnectionId}] disconnected.");
			return base.OnDisconnectedAsync(exception);
		}

		public async Task Handshake(string connectionId, string message)
		{
			await this.Clients.Caller.SendAsync("HandshakeResult", connectionId, "Hi!");
		}
		
		public async Task SendMessage(string connectionId, string message)
		{
			await this.Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
		}
		
		public async Task BroadcastMessage(string from, string message)
		{
			await this.Clients.All.SendAsync("ReceiveBroadcastMessage", from, message);
		}
	}
}