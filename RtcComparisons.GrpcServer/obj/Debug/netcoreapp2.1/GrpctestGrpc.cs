// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: grpctest.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Grpcserver {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class GrpcServerService
  {
    static readonly string __ServiceName = "grpcserver.GrpcServerService";

    static readonly grpc::Marshaller<global::Grpcserver.GrpcHandshakeRequest> __Marshaller_grpcserver_GrpcHandshakeRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpcserver.GrpcHandshakeRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Grpcserver.GrpcHandshakeResponse> __Marshaller_grpcserver_GrpcHandshakeResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpcserver.GrpcHandshakeResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Grpcserver.GrpcHandshakeRequest, global::Grpcserver.GrpcHandshakeResponse> __Method_Handshake = new grpc::Method<global::Grpcserver.GrpcHandshakeRequest, global::Grpcserver.GrpcHandshakeResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Handshake",
        __Marshaller_grpcserver_GrpcHandshakeRequest,
        __Marshaller_grpcserver_GrpcHandshakeResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Grpcserver.GrpctestReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GrpcServerService</summary>
    [grpc::BindServiceMethod(typeof(GrpcServerService), "BindService")]
    public abstract partial class GrpcServerServiceBase
    {
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Grpcserver.GrpcHandshakeResponse> Handshake(global::Grpcserver.GrpcHandshakeRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GrpcServerService</summary>
    public partial class GrpcServerServiceClient : grpc::ClientBase<GrpcServerServiceClient>
    {
      /// <summary>Creates a new client for GrpcServerService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public GrpcServerServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GrpcServerService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public GrpcServerServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected GrpcServerServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected GrpcServerServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Grpcserver.GrpcHandshakeResponse Handshake(global::Grpcserver.GrpcHandshakeRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Handshake(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Grpcserver.GrpcHandshakeResponse Handshake(global::Grpcserver.GrpcHandshakeRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Handshake, null, options, request);
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Grpcserver.GrpcHandshakeResponse> HandshakeAsync(global::Grpcserver.GrpcHandshakeRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HandshakeAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Grpcserver.GrpcHandshakeResponse> HandshakeAsync(global::Grpcserver.GrpcHandshakeRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Handshake, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override GrpcServerServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GrpcServerServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GrpcServerServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Handshake, serviceImpl.Handshake).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GrpcServerServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Handshake, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Grpcserver.GrpcHandshakeRequest, global::Grpcserver.GrpcHandshakeResponse>(serviceImpl.Handshake));
    }

  }
}
#endregion
