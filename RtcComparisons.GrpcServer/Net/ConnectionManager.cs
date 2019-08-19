using System;
using System.Collections.Generic;
using Grpc.Core;

namespace RtcComparisons.GrpcServer.Net
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
		
		public static Connection Open(ServerCallContext context)
		{
			Connection connection = new Connection(Guid.NewGuid().ToString(), context.Host);
			Connections.Add(connection.ConnectionId, connection);
			return connection;
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