using System;
 using RtcComparisons.Infrastructure.Services;
 using RtcComparisons.WebSocketServer.Services;
 
 namespace RtcComparisons.WebSocketServer
 {
 	class Program
 	{
 		static void Main(string[] args)
 		{
 			new Server().Initialize("http://localhost", 9716).Start();
 		}
 	}
 }