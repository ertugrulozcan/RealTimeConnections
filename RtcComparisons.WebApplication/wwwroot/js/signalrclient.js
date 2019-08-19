var signalrUrlInputBox = document.getElementById("signalrUrlInputBox");
var signalrOutputBox = document.getElementById("signalrOutputBox");
var signalrConnectButton = document.getElementById("signalrConnectButton");
var signalrMessageInputBox = document.getElementById("signalrMessageInputBox");
var signalrSendButton = document.getElementById("signalrSendButton");

var SignalRModule =
{
    log: function (text)
    {
        signalrOutputBox.innerHTML += "<span class='output'>" + "> " + text + "</span><br/>"
    },

    error: function (text)
    {
        signalrOutputBox.innerHTML += "<span class='output darkred'>" + "> " + text + "</span><br/>"
    },

    success: function (text)
    {
        signalrOutputBox.innerHTML += "<span class='output green'>" + "> " + text + "</span><br/>"
    }
};

function generateGuid()
{
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c)
    {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function generateUserId()
{
    return generateGuid().substring(28);
}

var signalrUserId = generateUserId();
var signalrConnection;

signalrConnectButton.addEventListener("click", function (event)
{
    signalrConnection = new signalR.HubConnectionBuilder().withUrl(signalrUrlInputBox.value).build();

    signalrConnection.on("HandshakeResult", function (user, message)
    {
        SignalRModule.success("Handshake success. UserID : " + user + ", Message : " + message);
    });

    signalrConnection.on("ReceiveBroadcastMessage", function (user, message)
    {
        SignalRModule.success("[" + user + "] " + message);
    });

    signalrConnection.start()
        .then(function()
        {
            SignalRModule.success("Connected.");

            signalrConnection
                .invoke("Handshake", signalrUserId, "Hello!")
                .catch(function(err)
                {
                    SignalRModule.error("Handshaking error. (" + err.toString() + ")");
                });

            SignalRModule.log("Handshake request sent.");
        })
        .catch(function(err)
        {
            SignalRModule.error("Connection failed. ");
            SignalRModule.error(err.stack);
            
            return console.error(err.toString());
        });    
});

signalrSendButton.addEventListener("click", function (event)
{
    signalrConnection
        .invoke("BroadcastMessage", signalrUserId, signalrMessageInputBox.value)
        .catch(function(err)
        {
            SignalRModule.error("Message could not sent!. (" + err.toString() + ")");
        });
});