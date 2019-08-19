module.exports = function (callback, x, y) 
{
    function generateConnectionId()
    {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c)
        {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
    
    try
    {
        const http = require('http');

        const hostname = 'localhost';
        const port = 9722;

        const server = http.createServer((req, res) =>
        {
            res.statusCode = 200;
            res.setHeader('Content-Type', 'text/plain');
            res.end('Hello World\n');
        });
        
        var io = require('socket.io')(port);
        io.on('connection', function (socket) 
        {
            socket.on('message', function (message) 
            {
                console.log('>>> ' + message);
                socket.broadcast.emit('message', message);
            });

            socket.on('handshake', function (message)
            {
                var connectionId = generateConnectionId().substring(28);
                console.log(connectionId);
                
                socket.send("Handshaked.");
                socket.send("Connection ID : " + connectionId);
                
                console.log('Handshake request : ' + message);
            });

            socket.on('event', data =>
            {
                console.log('A user connected.');
            });

            socket.on('disconnect', () =>
            {
                console.log('User disconnected.');
            });
        });

        //server.listen(3000);

        let result = `Server running at http://${hostname}:${port}/`;
        server.listen(port, hostname, () =>
        {
            console.log(result);
        });

        callback(null, result);
    }
    catch (ex) 
    {
        console.log(ex);
        callback(null, ex.message);
    }
};