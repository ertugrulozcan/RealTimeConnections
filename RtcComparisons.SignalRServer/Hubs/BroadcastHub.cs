using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using RtcComparisons.Infrastructure.Services;
using RtcComparisons.SignalRServer.Services.Interfaces;

namespace RtcComparisons.SignalRServer.Hubs
{
	public class BroadcastHub : Hub
	{
		private readonly IHubService hubService;
		
		public BroadcastHub()
		{
			this.hubService = ServiceLocator.Current.GetService<IHubService>();
		}

		public async Task Handshake(string connectionId, string message)
		{
			await this.Clients.Caller.SendAsync("HandshakeResult", connectionId, "Hi!");
		}
		
		public async Task SendMessage(string connectionId, string message)
		{
			await this.Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
		}
		
		public async Task BroadcastMessage(string connectionId, string message)
		{
			await this.Clients.All.SendAsync("ReceiveBroadcastMessage", connectionId, message);
		}
	}
}