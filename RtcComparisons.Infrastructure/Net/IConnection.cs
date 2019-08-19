using System;

namespace RtcComparisons.Infrastructure.Net
{
	public interface IConnection
	{
		#region Properties

		string ConnectionId { get; }
		
		#endregion

		#region Events

		event EventHandler Opened;
		event EventHandler Closed;
		event EventHandler<byte[]> ReceivedBinary;
		event EventHandler<string> ReceivedText;
		event EventHandler Sent;

		#endregion
	}
}