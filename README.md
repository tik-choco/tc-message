## TC Message
Unityで、message-driven processing（メッセージ駆動処理）を実現するプログラムです。


## Installation 
本Packageは、UniTaskが使用されています。
事前にImportしてください。
```
https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask
```

UPM Package

```
https://github.com/tik-choco/tc-message.git
```

## Usage
- Prefabs/TCMessage をScene上に配置

登録
```csharp
Message.Subscribe("○○", Method);
void Method() {}

Message.Subscribe<Vector3>("○○", Method);
void Method(Vector3 position) {}

Message.Subscribe<Vector3, int>("○○", Method);
int Method(Vector3 position) { return 0; }
```

呼び出し
```csharp
Message.Send("Method", args...);
var result = Message.Send<int>("Method", args...);
await Message.SendAsync("Method", args...);
```
