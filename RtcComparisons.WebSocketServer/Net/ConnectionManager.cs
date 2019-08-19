using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RtcComparisons.WebSocketServer.Net
{
	public static class ConnectionManager
	{
		#region Constants

		private static object threadLock = new object();

		#endregion
		
		#region Properties

		internal static Dictionary<string, Connection> Connections { get; } = new Dictionary<string, Connection>();

		#endregion

		#region Events

		public static event EventHandler Opened;
		public static event EventHandler Closed;
		public static event EventHandler<byte[]> ReceivedBinary;
		public static event EventHandler<string> ReceivedText;
		public static event EventHandler Sent;

		#endregion

		#region Methods

		public static void Configure(IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/ws")
				{
					if (context.WebSockets.IsWebSocketRequest)
					{
						var connection = Open(context);
					}
					else
					{
						context.Response.StatusCode = 400;
					}
				}
				else
				{
					await next();
				}
			});
		}
		
		public static Connection Open(HttpContext httpContext)
		{
			return OpenAsync(httpContext).Result;
		}
		
		public static async Task<Connection> OpenAsync(HttpContext httpContext)
		{
			using (WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync())
			{
				var connection = new Connection(webSocket, httpContext.Connection);
				connection.Opened += ConnectionOnOpened;
				connection.Closed += ConnectionOnClosed;
				connection.ReceivedBinary += ConnectionOnReceivedBinary;
				connection.ReceivedText += ConnectionOnReceivedText;
				connection.Sent += ConnectionOnSent;
				
				lock (threadLock)
				{
					Connections.Add(connection.ConnectionInfo.Id, connection);
				}
			
				connection.OnOpened();
			
				await Task.Run(() => connection.HandleRequest(httpContext, webSocket));

				return connection;
			}
		}

		public static Connection GetConnection(string id)
		{
			lock (threadLock)
			{
				if (Connections.ContainsKey(id))
					return Connections[id];

				return null;
			}
		}

		#endregion

		#region Event Handlers

		private static void ConnectionOnOpened(object sender, EventArgs e)
		{
			Opened?.Invoke(sender, e);
		}
		
		private static void ConnectionOnClosed(object sender, EventArgs e)
		{
			if (sender is Connection connection)
			{
				connection.Opened -= ConnectionOnOpened;
				connection.Closed -= ConnectionOnClosed;
				connection.ReceivedBinary -= ConnectionOnReceivedBinary;
				connection.ReceivedText -= ConnectionOnReceivedText;
				connection.Sent -= ConnectionOnSent;
				
				Closed?.Invoke(connection, e);
			}
		}

		private static void ConnectionOnReceivedBinary(object sender, byte[] e)
		{
			ReceivedBinary?.Invoke(sender, e);
		}
		
		private static void ConnectionOnReceivedText(object sender, string e)
		{
			ReceivedText?.Invoke(sender, e);
		}
		
		private static void ConnectionOnSent(object sender, EventArgs e)
		{
			Sent?.Invoke(sender, e);
		}
		
		#endregion
	}
}