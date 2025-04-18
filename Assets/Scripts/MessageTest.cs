using System.Threading.Tasks;
using UnityEngine;

namespace TC
{
    public class MessageTest : MonoBehaviour
    {
        private async void Start()
        {
            Logger.LogLevel = Logger.Level.Debug;
            Message.Register("Test", TestFunc);
            Message.Register("Test2", TestAsyncFunc);
            Message.Call("Test");
            await Message.CallAsync("Test2");
        }

        private void TestFunc()
        {
            Logger.Debug("TestFunc called");
            Logger.Info("TestFunc called");
            Logger.Warning("TestFunc called");
            Logger.Error("TestFunc called");
            Logger.Fatal("TestFunc called");
        }

        private async Task TestAsyncFunc()
        {
            Logger.Debug("TestAsyncFunc called");
            await Task.Delay(1000);
            Logger.Info("TestAsyncFunc called");
            await Task.Delay(1000);
            Logger.Warning("TestAsyncFunc called");
            await Task.Delay(1000);
            Logger.Error("TestAsyncFunc called");
            await Task.Delay(1000);
            Logger.Fatal("TestAsyncFunc called");
        }
    }
}
