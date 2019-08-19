using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RtcComparisons.Infrastructure.Net;

namespace RtcComparisons.WebSocketServer.Net
{
	public class Connection : IConnection
	{
		#region Constants

		private const int DEFAULT_BUFFER_LENGTH = 1024 * 4;

		#endregion

		#region Properties

		private WebSocket Socket { get; }
		
		public ConnectionInfo ConnectionInfo { get; }

		public string ConnectionId
		{
			get
			{
				return this.ConnectionInfo?.Id;
			}
		}

		public bool SendReceivedResponseForEachRequest { get; } = false;

		#endregion

		#region Events

		public event EventHandler Opened;
		public event EventHandler Closed;
		public event EventHandler<byte[]> ReceivedBinary;
		public event EventHandler<string> ReceivedText;
		public event EventHandler Sent;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="webSocket"></param>
		/// <param name="httpConnection"></param>
		internal Connection(WebSocket webSocket, ConnectionInfo httpConnection)
		{
			this.Socket = webSocket;
			this.ConnectionInfo = httpConnection;
		}

		#endregion

		#region Methods

		internal async Task HandleRequest(HttpContext context, WebSocket webSocket)
		{
			var buffer = new byte[DEFAULT_BUFFER_LENGTH];
			WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			while (!result.CloseStatus.HasValue)
			{
				string connectionId = context.Connection.Id;
				if (this.ConnectionId != connectionId)
					continue;
				
				switch (result.MessageType)
				{
					case WebSocketMessageType.Binary:
					{
						this.OnReceivedBinary(buffer);
					}
					break;
					
					case WebSocketMessageType.Text:
					{
						string text = Encoding.UTF8.GetString(buffer);
						if (text.StartsWith("{{{HANDSHAKE_REQUEST}}}"))
						{
							this.HandshakeWithClient();
							break;
						}
						
						this.OnReceivedText(text);
					}
					break;
					
					case WebSocketMessageType.Close:
					{
						Console.WriteLine("Connection closed.");
					}
					break;
					
					default:
					{
						Console.WriteLine("Connection closed.");
					}
					break;
				}

				if (this.SendReceivedResponseForEachRequest)
					this.SendReceivedResponse();

				buffer = new byte[DEFAULT_BUFFER_LENGTH];
				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			}

			await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
			this.OnClosed();
		}

		private void HandshakeWithClient()
		{
			this.SendTextAsync("{{{HANDSHAKE_RESPONSE:BEGIN}}}");
			this.SendTextAsync(this.ConnectionId);
			this.SendTextAsync("{{{HANDSHAKE_RESPONSE:END}}}");
		}

		internal void OnOpened()
		{
			Console.WriteLine($"Connection opened. (ConnectionId : '{this.ConnectionInfo.Id}')");
			this.Opened?.Invoke(this, new EventArgs());
		}
		
		internal void OnClosed()
		{
			Console.WriteLine("Connection closed.");
			this.Closed?.Invoke(this, new EventArgs());
		}

		internal void OnReceivedText(string text)
		{
			Console.WriteLine($"Received a text message : '{text}' from {this.ConnectionId}");
			this.ReceivedText?.Invoke(this, text);
			
			this.BroadcastMessage(text);
		}

		internal void OnReceivedBinary(byte[] buffer)
		{
			Console.WriteLine($"Received a binary message from {this.ConnectionId}");
			this.ReceivedBinary?.Invoke(this, buffer);
		}

		public void Send(string message, string recipientId)
		{
			var targetConnection = ConnectionManager.GetConnection(recipientId);
			targetConnection?.SendTextAsync(message);
		}

		private async void SendTextAsync(string message)
		{
			if (this.Socket == null)
				return;
			
			try
			{
				var buffer = Encoding.UTF8.GetBytes(message);
				await this.Socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
		
		/// <summary>
		/// Send message received response
		/// </summary>
		private async void SendReceivedResponse()
		{
			if (this.Socket == null)
				return;
			
			try
			{
				var buffer = Encoding.UTF8.GetBytes("Received.");
				await this.Socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private void BroadcastMessage(string message)
		{
			foreach (var connection in ConnectionManager.Connections)
			{
				this.Send(message, connection.Key);
			}
		}

		#endregion
	}
}