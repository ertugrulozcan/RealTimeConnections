using System;
using RtcComparisons.SignalRServer.Services;

namespace RtcComparisons.SignalRServer
{
	class Program
	{
		static void Main(string[] args)
		{
			new Server().Initialize("http://localhost", 9718).Start();
		}
	}
}