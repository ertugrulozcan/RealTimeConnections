using System;
using RtcComparisons.Infrastructure.Services;
using WebSockets = RtcComparisons.WebSocketServer.Services;
using SignalR = RtcComparisons.SignalRServer.Services;
using GRPC = RtcComparisons.GrpcServer.Services;

namespace RtcComparisons.DataTransferTest
{
	class Program
	{
		static void Main(string[] args)
		{
			IServer webSocketServer = new WebSockets.Server().Initialize("http://localhost", 9716).Start();
			IServer signalrServer = new SignalR.Server().Initialize("http://localhost", 9718).Start();
			IServer grpcServer = new GRPC.Server().Initialize("http://localhost", 9720).Start();
		}
	}
}