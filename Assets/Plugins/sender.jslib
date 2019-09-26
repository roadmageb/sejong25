mergeInto(LibraryManager.library, {
    _ConnectServer: function() {
        var script = document.createElement('script');
        script.onload = function () {
            window.socket = io.connect();
            socket.on('data', function(msg){
                unityInstance.SendMessage('SocketIO', 'OnReceive', JSON.stringify(msg));
            });
        };
        script.src = '/socket.io/socket.io.js';

        document.head.appendChild(script);
    },

    _SendData: function(id, data) {
        //console.log([Pointer_stringify(id), Pointer_stringify(data)]);
        socket.emit(Pointer_stringify(id), JSON.parse(Pointer_stringify(data)));
    }
})