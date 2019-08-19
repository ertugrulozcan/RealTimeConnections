using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpcserver;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RtcComparisons.Infrastructure.Services;

namespace RtcComparisons.GrpcServer.Services
{
	public class Server : IServer
	{
		#region Properties

		private IServiceCollection Services { get; set; }
		
		private Grpc.Core.Server CoreServer { get; set; }
		
		private string ServerUrl { get; set; }

		#endregion
		
		#region Events

		public event EventHandler Started;
		public event EventHandler Ended;

		#endregion

		#region Methods

		public IServer Initialize(string host, int port)
		{
			var grpcHost = host;
			if (host.StartsWith("http://"))
				grpcHost = host.Substring("http://".Length);

			this.ServerUrl = grpcHost + ":" + port;
			
			this.CoreServer = new Grpc.Core.Server
			{
				Services = { GrpcServerService.BindService(new GrpcService()) },
				Ports = { new ServerPort(grpcHost, port, ServerCredentials.Insecure) }
			};

			return this;
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors("CorsPolicy");
			ServiceLocator.Current = this.Services.BuildServiceProvider();
			
			/*
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				// Communication with gRPC endpoints must be made through a gRPC client.
				// To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
				endpoints.MapGrpcService<GrpcService>();
			});
			*/
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
			
			// this.Services.AddGrpc();
		}
		
		public IServer Start()
		{
			try
			{
				CancellationTokenSource tokenSource = new CancellationTokenSource();
				var serverTask = RunServiceAsync(this.CoreServer, tokenSource.Token);
				
				this.Started?.Invoke(this, new EventArgs());
				
				Console.WriteLine("GrpcServer listening on port " + this.ServerUrl);
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

		private static async Task RunServiceAsync(Grpc.Core.Server server, CancellationToken cancellationToken = default(CancellationToken))
		{
			// Start server
			server.Start();

			await AwaitCancellation(cancellationToken);
			await server.ShutdownAsync();
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