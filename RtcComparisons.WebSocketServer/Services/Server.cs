using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using RtcComparisons.Infrastructure.Services;
using RtcComparisons.WebSocketServer.Net;

namespace RtcComparisons.WebSocketServer.Services
{
	public class Server : IServer
	{
		#region Properties

		private IWebHost Host { get; set; }

		#endregion
		
		#region Events

		public event EventHandler Started;
		public event EventHandler Ended;

		#endregion
		
		#region Methods

		public IServer Initialize(string host, int port)
		{
			var webHostBuilder = WebHost.CreateDefaultBuilder();
			webHostBuilder.UseStartup<Server>();
			webHostBuilder.UseUrls($"{host}:{port}");
			this.Host = webHostBuilder.Build();
			
			return this;
		}
		
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseWebSockets();
			ConnectionManager.Configure(app);
		}
		
		public IServer Start()
		{
			try
			{
				this.Host.Run();
				this.Started?.Invoke(this, new EventArgs());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}

			return this;
		}

		public void OnMessageReceived(Action<string> action)
		{
			ConnectionManager.ReceivedText += delegate(object sender, string message) { action?.Invoke(message); };
		}
		
		#endregion
	}
}