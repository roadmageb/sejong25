var express = require("express");
var app = express();
var server = require("http").Server(app);
var io = require("socket.io")({
    transports: ["websocket"]
}).listen(server);

app.use("/Build", express.static(__dirname + "/Build"));
app.use("/TemplateData", express.static(__dirname + "/TemplateData"));

app.get("/", function(req, res) {
    res.sendFile(__dirname + "/index.html");
});

server.listen(80, function() {
    console.log("[SERVER] Listening on port " + server.address().port);
});

io.on("connection", function(socket) {
    console.log(socket.id + " login");

    // connection test
    socket.on("myPing", function(data) {
        socket.emit("data", { id:"myPong", data: { hello: "Hello Client!" }});
    });
    socket.on("myPong", function(data) {
        console.log("PING PONG Done", data);
    });

    // gameRoom join / exit
    socket.on('joinRoom', function(data) {
        GameRoomFunc.joinRoom(socket, data.roomId, data.hopaeName);
        socket.emit('data', {id: 'joinTo', data: { roomId: socket.roomId}});
    });
    socket.on('exitRoom', function(data) {
        GameRoomFunc.exitRoom(socket);
    });

    // before start
    // socket.on('')

    // ingame playing
    socket.on('attack', function(data){

    });
    socket.on('defeat', function(data){

    });

    socket.on('disconnect', function(){
        if (socket.roomId != undefined) {
            GameRoomFunc.exitRoom(socket);
        }
        console.log(socket.id + ' logout');
    })
});

/**
 * Game Room Code from here
 */
var GameRoomFunc = GameRoomFunc || {};

GameRoomFunc.roomNums = [];
GameRoomFunc.rooms = {};

GameRoomFunc.generateRoomId = function(roomData) {
    for (let i = 0; i < 30; ++i)
    {
        let num = Math.floor(Math.random() * 100000);
        if (GameRoomFunc.roomNums.findIndex(function(element){
            return element === num;
        }) === -1) {    
            this.roomNums.push(num);
            Object.defineProperty(this.rooms, num, {value: roomData});
            return num;
        }
    }
    console.error('[ERROR] too many room in here, refuse generate room');
    return null;
}

GameRoomFunc.quickMatch = function() {
    for (let i = 0; i < this.roomNums.length; i++) {
        let roomId = this.roomNums[i];
        if (this.rooms[roomId].playerCount < this.rooms[roomId].maxPlayer) {
            return roomId;
        }
    }
    let newRoom = new GameRoom();
    return newRoom.roomId;
}

GameRoomFunc.joinRoom = function(_socket, roomId, _hopaeName) {
    let toJoin = roomId > 0 ? roomId : this.quickMatch();
    Object.defineProperty(this.rooms[toJoin].players, _socket.id, {value: {data: new Player(_socket, _hopaeName), socket: _socket}});
    _socket.roomId = toJoin;
    _socket.join(toJoin);
    _socket.broadcast.to(toJoin).emit('data', {id: 'joinRoom', data: {userId: _socket.id, hopaeName: _hopaeName}});
    console.log(_socket.id + ' enter to ' + _socket.roomId);
}

GameRoomFunc.exitRoom = function(socket) {
    if (socket.roomId != undefined) {
        try {
            socket.broadcast.to(socket.roomId).emit('data', {id: 'exitRoom', data:{userId: socket.id}});
            console.log(socket.id + ' exit from ' + socket.roomId);
            delete this.rooms[socket.roomId].players[socket.id];
            delete socket.roomId;
        }
        catch (e) {
            console.error(e);
        }
    } else {
        console.error('[ERROR] '+socket.id+' is not in room');
    }
}


class GameRoom{
    constructor() {
        this.roomId = GameRoomFunc.generateRoomId(this);

        this.maxPlayer = 25;
        this.initPlayer = 5;

        this.players = {};
        this.playerCount = 0;

        //this.phase = 
    }
};

class Player {
    constructor(socket, _hopaeName) {
        this.id = socket.id;
        this.hopaeName = _hopaeName;
    }
};