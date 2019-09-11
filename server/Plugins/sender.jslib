mergeInto(LibraryManager.library, {
    _SendData: function(id, data) {
        console.log({Pointer_stringify(id), Pointer_stringify(data)});

        socket.emit(Pointer_stringify(id), Pointer_stringify(data));
    }
})