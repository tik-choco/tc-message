## TC Message
Unityで、message-driven processing（メッセージ駆動処理）を実現するプログラムです。


## Installation 

UPM Package

```
https://github.com/tik-choco/tc-message.git?path=/Assets/
```

## Usage

登録
```csharp
Message.Register("○○", Method);
void Method() {}

Message.Register<Vector3>("○○", Method);
void Method(Vector3 position) {}

Message.Register<Vector3, int>("○○", Method);
int Method(Vector3 position) { return 0; }
```

呼び出し
```csharp
Message.Call("Method", args...);
var result = Message.Call<int>("Method", args...);
await Message.CallAsync("Method", args...);
```
