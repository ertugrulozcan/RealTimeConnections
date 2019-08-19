/**
 * @fileoverview gRPC-Web generated client stub for grpcserver
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!



const grpc = {};
grpc.web = require('grpc-web');

const proto = {};
proto.grpcserver = require('./grpctest_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.grpcserver.GrpcServerServiceClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

  /**
   * @private @const {?Object} The credentials to be used to connect
   *    to the server
   */
  this.credentials_ = credentials;

  /**
   * @private @const {?Object} Options for the client
   */
  this.options_ = options;
};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.grpcserver.GrpcServerServicePromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

  /**
   * @private @const {?Object} The credentials to be used to connect
   *    to the server
   */
  this.credentials_ = credentials;

  /**
   * @private @const {?Object} Options for the client
   */
  this.options_ = options;
};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.grpcserver.GrpcHandshakeRequest,
 *   !proto.grpcserver.GrpcHandshakeResponse>}
 */
const methodDescriptor_GrpcServerService_Handshake = new grpc.web.MethodDescriptor(
  '/grpcserver.GrpcServerService/Handshake',
  grpc.web.MethodType.UNARY,
  proto.grpcserver.GrpcHandshakeRequest,
  proto.grpcserver.GrpcHandshakeResponse,
  /** @param {!proto.grpcserver.GrpcHandshakeRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.grpcserver.GrpcHandshakeResponse.deserializeBinary
);


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.grpcserver.GrpcHandshakeRequest,
 *   !proto.grpcserver.GrpcHandshakeResponse>}
 */
const methodInfo_GrpcServerService_Handshake = new grpc.web.AbstractClientBase.MethodInfo(
  proto.grpcserver.GrpcHandshakeResponse,
  /** @param {!proto.grpcserver.GrpcHandshakeRequest} request */
  function(request) {
    return request.serializeBinary();
  },
  proto.grpcserver.GrpcHandshakeResponse.deserializeBinary
);


/**
 * @param {!proto.grpcserver.GrpcHandshakeRequest} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.Error, ?proto.grpcserver.GrpcHandshakeResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.grpcserver.GrpcHandshakeResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.grpcserver.GrpcServerServiceClient.prototype.handshake =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/grpcserver.GrpcServerService/Handshake',
      request,
      metadata || {},
      methodDescriptor_GrpcServerService_Handshake,
      callback);
};


/**
 * @param {!proto.grpcserver.GrpcHandshakeRequest} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.grpcserver.GrpcHandshakeResponse>}
 *     A native promise that resolves to the response
 */
proto.grpcserver.GrpcServerServicePromiseClient.prototype.handshake =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/grpcserver.GrpcServerService/Handshake',
      request,
      metadata || {},
      methodDescriptor_GrpcServerService_Handshake);
};


module.exports = proto.grpcserver;

