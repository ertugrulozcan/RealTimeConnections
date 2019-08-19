using System;

namespace RtcComparisons.Infrastructure.Services
{
	public interface IServer
	{
		#region Events

		event EventHandler Started;
		event EventHandler Ended;

		#endregion

		#region Methods

		IServer Initialize(string host, int port);
		
		IServer Start();

		void OnMessageReceived(Action<string> action);

		#endregion
	}
}