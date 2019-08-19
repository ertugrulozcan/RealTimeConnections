using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using RtcComparisons.SignalRServer.Services.Interfaces;

namespace RtcComparisons.SignalRServer.Services
{
	public class HubService : IHubService
	{
		#region Services

		private readonly IApplicationBuilder applicationBuilder;

		#endregion
		
		#region Events

		public event EventHandler<string> ReceivedMessage;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="app"></param>
		public HubService(IApplicationBuilder app)
		{
			this.applicationBuilder = app;
		}

		#endregion
		
		#region Methods

		public void RegisterHub<THub>(string hubName) where THub : Hub
		{
			this.applicationBuilder.UseSignalR(routes =>
			{
				routes.MapHub<THub>(hubName);
			});
		}

		#endregion
	}
}