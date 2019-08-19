var nodejsUrlInputBox = document.getElementById("nodejsUrlInputBox");
var nodejsOutputBox = document.getElementById("nodejsOutputBox");
var nodejsConnectButton = document.getElementById("nodejsConnectButton");
var nodejsMessageInputBox = document.getElementById("nodejsMessageInputBox");
var nodejsSendButton = document.getElementById("nodejsSendButton");

var NodeJSModule =
{
    log: function (text)
    {
        nodejsOutputBox.innerHTML += "<span class='output'>" + "> " + text + "</span><br/>"
    },

    error: function (text)
    {
        nodejsOutputBox.innerHTML += "<span class='output darkred'>" + "> " + text + "</span><br/>"
    },

    success: function (text)
    {
        nodejsOutputBox.innerHTML += "<span class='output green'>" + "> " + text + "</span><br/>"
    }
};

var socket;

nodejsConnectButton.addEventListener("click", function (event)
{
    socket = io(nodejsUrlInputBox.value);
    socket.on('connect', function () 
    {
        socket.emit('handshake', 'Hi!');

        socket.on('message', function (msg) 
        {
            NodeJSModule.log(msg);
        });
    });
});

nodejsSendButton.addEventListener("click", function (event)
{
    socket.emit('message', nodejsMessageInputBox.value);
});