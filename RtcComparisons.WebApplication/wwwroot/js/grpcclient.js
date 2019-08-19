const {GrpcHandshakeRequest, GrpcHandshakeResponse, GrpcRequest, GrpcResponse} = require('./grpctest_pb.js');
const {GrpcServerService} = require('./grpctest_grpc_web_pb.js');

var grpcService = new GrpcServerService('http://localhost:1920');

var request = new GrpcHandshakeRequest();
grpcService.handshake(request, {}, function(err, response) 
{
    // ...
});
