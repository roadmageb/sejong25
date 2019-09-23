var GameRoomFunc = GameRoomFunc || {};

GameRoomFunc.roomNums = [];
GameRoomFunc.generateRoomId = function() {
    for (let i = 0; i < 30; ++i)
    {
        let num = Math.floor(Math.random() * 100000);
        if (GameRoomFunc.roomNums.findIndex(function(element){
            return element === num;
        }) === -1) {
            this.roomNums.push(num);
            return num;
        }
    }
    console.error('[ERROR] too many room in here, refuse generate room');
}

GameRoomFunc.rooms = [];
GameRoomFunc.assignRoom = function(roomData) {
    for (let i = 0; i < this.rooms.length; ++i)
    {
        if (this.rooms[i] === null) {
            this.rooms[i] = roomData;
            return i;
        }
    }
    return this.rooms.push(roomData) - 1;
}

GameRoomFunc.joinRoom = function(socket, roomId) {
    let toJoin = roomId > 0 ? roomId : (new GameRoom()).roomId;
    socket.roomId = roomId;
    socket.join(toJoin);
}

class GameRoom {
    constructor() {
        this.roomId = GameRoomFunc.generateRoomId();
        this.roomIdx = GameRoomFunc.assignRoom(this);

        this.maxPlayer = 25;
        this.initPlayer = 5;
    }
}

class Player {
    constructor(socket) {
        this.id = socket.id;
        this.name = '홍길동';
    }
}