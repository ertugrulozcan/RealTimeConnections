using System;
using Microsoft.AspNetCore.SignalR;

namespace RtcComparisons.SignalRServer.Services.Interfaces
{
	public interface IHubService
	{
		void RegisterHub<THub>(string hubName) where THub : Hub;
		
		event EventHandler<string> ReceivedMessage;
	}
}