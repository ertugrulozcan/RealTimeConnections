using System;
using RtcComparisons.Infrastructure.Net;

namespace RtcComparisons.SignalRServer.Net
{
	public class Connection : IConnection
	{
		#region Properties

		public string ConnectionId
		{
			get;
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