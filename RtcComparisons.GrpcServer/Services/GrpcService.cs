using System.Threading.Tasks;
using Grpc.Core;
using Grpcserver;
using RtcComparisons.GrpcServer.Net;

namespace RtcComparisons.GrpcServer.Services
{
	public class GrpcService : GrpcServerService.GrpcServerServiceBase
	{
		// Server side handler of the SayHello RPC
		public override Task<GrpcHandshakeResponse> Handshake(GrpcHandshakeRequest request, ServerCallContext context)
		{
			var connection = ConnectionManager.Open(context);
			return Task.FromResult(new GrpcHandshakeResponse { UserId = connection.ConnectionId });
		}
	}
}