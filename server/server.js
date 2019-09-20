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
    console.log("someone login");

    socket.on("myPing", function(data) {
        socket.emit("myPong", { hello: "Hello Client!" });
    });
    socket.on("myPong", function(data) {
        console.log("PING PONG Done", data);
    });
});
