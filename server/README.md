# WebSocket With Unity WebGL 사용법

## 서버 여는법

0. Unity의 씬에 SocketIO 오브젝트가 반드시 존재해야 합니다.
1. Unity에서 WebGL형태로 빌드합니다.
1. 빌드가 다될때까지 기다립니다.
1. 빌드된 폴더에 gitRepo/server/ 안의 모든 파일들을 복사해서 넣습니다.
1. node를 설치합니다.
1. 빌드 폴더에서 cmd를 켠 뒤 다음 커맨드를 입력합니다.

```cmd
    $ node server.js
```

6. 이후 입력되는 [SERVER] 어쩌구가 보이면 성공입니다.

## 서버 접속법

1. 주소창에 localhost치고 들어가세요

---

## 소켓 사용법 및 구조

기본적인 구조는 Unity의 SocketManager 코드를 참고하면 됩니다.

-   emit 사용법

SocketManager 의 instance를 불러온 뒤, 그 안의 SendData(string id, JObject data)를 이용하면 됩니다.  
JObject는 js의 프리한 그 구조를 가져오는 것으로, 만일 비어있는 채로 보내고싶으면 SocketManager의 emptyObj를 보내면 됩니다.  
원하는 데이터를 보내고싶을때는,

```C#
using Newtonsoft.Json.Linq;

...
var objToSend = new JObject();
objToSend.add("targetId", "123456");
objToSend.add("senderId", "654321");
objToSned.add("include", 19);

SocketManager.inst.SendData("sendId", objToSend);
...
```

와 같이 사용하면 됩니다.  
오브젝트끼리 중첩 가능하며, 그냥 js에서 오브젝트 쓰듯이 하면 됩니다.  
https://devstarsj.github.io/development/2016/06/11/CSharp.NewtonJSON/ 에 더 많은 정보가 있습니다.

-   on 사용법

SocketManager의 instance에 onReceieve라는 콜백이 있습니다.  
여기에 원하는 콜백함수를 추가하면 됩니다.

```C#
SocketManager.inst.onReceive += (string id, JObject data) =>
{
    if (id == "sendId")
    {
        Debug.Log(id + ": " + data.ToString());
    }
}
```

돌아온 데이터도 JObject 데이터 형태입니다. 이걸 읽는법은

```C#
var targetId = data["targetId"]; // value is "123456"
var senderId = data["senderId"]; // value is "654321"
var include = data["include"]; // value is 19
```

으로 읽으면 됩니다.  
콜백형태이므로, 함수 이름을 지정해두면 제거도 가능합니다.
