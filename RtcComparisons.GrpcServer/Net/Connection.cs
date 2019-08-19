using System;
using RtcComparisons.Infrastructure.Net;

namespace RtcComparisons.GrpcServer.Net
{
	public class Connection : IConnection
	{
		#region Properties

		public string ConnectionId
		{
			get;
		}
		
		public string Host
		{
			get;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionId"></param>
		/// <param name="host"></param>
		public Connection(string connectionId, string host)
		{
			this.ConnectionId = connectionId;
			this.Host = host;
		}

		#endregion

		#region Events

		public event EventHandler Opened;
		public event EventHandler Closed;
		public event EventHandler<byte[]> ReceivedBinary;
		public event EventHandler<string> ReceivedText;
		public event EventHandler Sent;

		#endregion
	}
}