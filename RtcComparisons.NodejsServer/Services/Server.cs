using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using RtcComparisons.Infrastructure.Services;

namespace RtcComparisons.NodejsServer.Services
{
	public class Server : IServer
	{
		#region Properties

		private IServiceCollection Services { get; set; }
		
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

			app.UseCors("CorsPolicy");
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			ServiceLocator.Current = services.BuildServiceProvider();
			
			services.AddCors(options => options.AddPolicy("CorsPolicy", builder => 
			{
				builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithOrigins("http://localhost:5000")
					.AllowCredentials()
					.SetIsOriginAllowed((host) => true);
			}));
			
			services.AddNodeServices();
			this.Services = services;
		}
		
		public IServer Start()
		{
			try
			{
				CancellationTokenSource tokenSource = new CancellationTokenSource();
				var serverTask = RunServiceAsync(tokenSource.Token);

				//this.Host.Run();
				this.Started?.Invoke(this, new EventArgs());
				
				Console.WriteLine("Press any key to stop the server...");
				Console.ReadKey();

				tokenSource.Cancel();
				Console.WriteLine("Shutting down...");
				serverTask.Wait();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
			
			return this;
		}
		
		private static Task AwaitCancellation(CancellationToken token)
		{
			var taskSource = new TaskCompletionSource<bool>();
			token.Register(() => taskSource.SetResult(true));
			return taskSource.Task;
		}

		private static async Task RunServiceAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			// Start server
			INodeServices nodeService = ServiceLocator.Current.GetService<INodeServices>();
			if (nodeService == null)
			{
				nodeService = NodeServicesFactory.CreateNodeServices(new NodeServicesOptions(ServiceLocator.Current));
			}
				
			string result = nodeService.InvokeAsync<string>("server.js").Result;
			PrintNodeJsOutput(result);

			await AwaitCancellation(cancellationToken);
			//await nodeService.InvokeAsync<string>("Shutdown").Result;
		}

		private static void PrintNodeJsOutput(string text)
		{
			Console.WriteLine("NODEJS> " + text);
		}
		
		#endregion

		#region Event Handlers

		public void OnMessageReceived(Action<string> action)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}