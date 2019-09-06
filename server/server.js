var express = require('express');
var app = express();
var server = require('http').Server(app);
var io = require('socket.io').listen(server);

app.use('/js', express.static(__dirname + '/js'));
app.use('/Build', express.static(__dirname + '/Build'));
app.use('/TemplateData', express.static(__dirname + '/TemplateData'));

app.get('/', function(req, res) {
    res.sendFile(__dirname + '/index.html');
});

server.listen(80, function() {
    console.log(new Date().toLocaleTimeString('ko-KR') + ' [SERVER] Listening on port ' + server.address().port);
});

io.on('connection', function(socket){
    socket.on('login', function(){
        console.log('logined');
        socket.emit('data', {id:'ping', data:{}});
    })

    socket.on('pong', function(){
        console.log('pingPong');
    })
});
