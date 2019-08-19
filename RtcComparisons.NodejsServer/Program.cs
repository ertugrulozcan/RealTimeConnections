using System;
using RtcComparisons.NodejsServer.Services;

namespace RtcComparisons.NodejsServer
{
	class Program
	{
		static void Main(string[] args)
		{
			new Server().Initialize("http://localhost", 9722).Start();
		}
	}
}