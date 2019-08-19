var websocketUrlInputBox = document.getElementById("websocketUrlInputBox");
var websocketOutputBox = document.getElementById("websocketOutputBox");
var websocketConnectButton = document.getElementById("websocketConnectButton");
var websocketMessageInputBox = document.getElementById("websocketMessageInputBox");
var websocketSendButton = document.getElementById("websocketSendButton");

var WebSocketModule =
{
    log: function (text)
    {
        websocketOutputBox.innerHTML += "<span class='output'>" + "> " + text + "</span><br/>"
    },

    error: function (text)
    {
        websocketOutputBox.innerHTML += "<span class='output darkred'>" + "> " + text + "</span><br/>"
    },

    success: function (text)
    {
        websocketOutputBox.innerHTML += "<span class='output green'>" + "> " + text + "</span><br/>"
    },
    
    HandshakeRequest: function () 
    {
        isHandshaking = true;
        socket.send("{{{HANDSHAKE_REQUEST}}}");
    }
};

function updateState()
{
    function disable()
    {
        websocketMessageInputBox.disabled = true;
        websocketSendButton.disabled = true;
    }

    function enable()
    {
        websocketMessageInputBox.disabled = false;
        websocketSendButton.disabled = false;
    }

    websocketUrlInputBox.disabled = true;
    websocketConnectButton.disabled = true;

    if (!socket)
    {
        disable();
    }
    else
    {
        switch (socket.readyState)
        {
            case WebSocket.CLOSED:
                WebSocketModule.log("Closed.");
                disable();
                websocketUrlInputBox.disabled = false;
                websocketConnectButton.disabled = false;
                break;
            case WebSocket.CLOSING:
                WebSocketModule.log("Closing...");
                disable();
                break;
            case WebSocket.CONNECTING:
                WebSocketModule.log("Connecting...");
                disable();
                break;
            case WebSocket.OPEN:
                WebSocketModule.log("Connection open.");
                enable();
                break;
            default:
                WebSocketModule.error("Unknown WebSocket State: " + htmlEscape(socket.readyState));
                disable();
                break;
        }
    }
}

function htmlEscape(str)
{
    return str.toString()
        .replace(/&/g, '&amp;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');
}

var socket;
var isHandshaking = false;
var userId;

websocketConnectButton.onclick = function() 
{
    WebSocketModule.log("Connecting...");
    
    socket = new WebSocket(websocketUrlInputBox.value);
    socket.onopen = function (event) 
    {
        updateState();
        
        WebSocketModule.HandshakeRequest();
    };
    
    socket.onclose = function (event) 
    {
        updateState();
        WebSocketModule.error("Connection closed. Code: " + htmlEscape(event.code) + ". Reason: " + htmlEscape(event.reason));
    };
    
    socket.onerror = function (event)
    {
        updateState();
        WebSocketModule.error("Connection closed. Code: " + htmlEscape(event.code) + ". Reason: " + htmlEscape(event.reason));
    };
    
    socket.onmessage = function (event) 
    {
        if (isHandshaking)
        {
            if (htmlEscape(event.data) === "{{{HANDSHAKE_RESPONSE:BEGIN}}}")
            {
                WebSocketModule.log("Handshaking...");
                return;
            }
            else if (htmlEscape(event.data) === "{{{HANDSHAKE_RESPONSE:END}}}")
            {
                isHandshaking = false;
                WebSocketModule.success("Connected. (UserID : " + userId + ")");
                return;
            }
            else 
            {
                userId = htmlEscape(event.data);
                return;
            }
        }
        
        WebSocketModule.log(htmlEscape(event.data));
    };
};

websocketSendButton.onclick = function()
{
    if (!socket || socket.readyState !== WebSocket.OPEN) 
    {
        WebSocketModule.error("Socket not connected, message could not be sent.");
    }
    
    var data = websocketMessageInputBox.value;
    socket.send(data);

    WebSocketModule.log("[" + userId + "] " + htmlEscape(data));
};