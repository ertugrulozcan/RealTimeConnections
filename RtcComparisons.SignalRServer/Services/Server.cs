using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RtcComparisons.Infrastructure.Services;
using RtcComparisons.SignalRServer.Hubs;
using RtcComparisons.SignalRServer.Services.Interfaces;

namespace RtcComparisons.SignalRServer.Services
{
	public class Server : IServer
	{
		#region Services

		private IHubService hubService;

		#endregion
		
		#region Properties

		private IWebHost Host { get; set; }
		
		private IServiceCollection Services { get; set; }

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

			app.UseCors("CorsPolicy");
			
			this.hubService = new HubService(app);
			this.Services.AddSingleton(this.hubService);
			this.hubService.RegisterHub<BroadcastHub>("/hub");

			ServiceLocator.Current = this.Services.BuildServiceProvider();
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			this.Services = services;
			
			this.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => 
			{
				builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithOrigins("http://localhost:5000")
					.AllowCredentials()
					.SetIsOriginAllowed((host) => true);
			}));
			
			this.Services.AddSignalR();
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
			this.hubService.ReceivedMessage += delegate(object sender, string message) { action?.Invoke(message); };
		}

		#endregion
	}
}