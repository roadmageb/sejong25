mergeInto(LibraryManager.library, {
    _ConnectServer: function() {
        try{
            window.socket = io.connect();
            socket.on('data', function(msg){
                unityInstance.SendMessage('SocketIO', 'OnReceive', JSON.stringify(msg));
            });
        }
        catch(e){
            console.error('failed connect');
        }
    },

    _SendData: function(id, data) {
        console.log([Pointer_stringify(id), Pointer_stringify(data)]);

        socket.emit(Pointer_stringify(id), Pointer_stringify(data));
    }
})