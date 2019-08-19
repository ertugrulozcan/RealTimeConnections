using System;
using RtcComparisons.GrpcServer.Services;

namespace RtcComparisons.GrpcServer
{
	class Program
	{
		static void Main(string[] args)
		{
			new Server().Initialize("http://localhost", 9720).Start();
		}
	}
}