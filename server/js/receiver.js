var socket = io.connect();
var unityInstance = UnityLoader.instantiate("unityContainer", "Build/Sejong25Neo.json", {onProgress: UnityProgress});

window.socket = socket;
socket.emit('login');

socket.on('data', function(msg){
    let toSend = JSON.stringify(msg);
    unityInstance.SendMessage('SocketIO', 'OnReceive', toSend);
})