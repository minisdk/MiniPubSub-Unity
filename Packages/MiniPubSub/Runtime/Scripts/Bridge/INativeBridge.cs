
namespace MiniSDK.PubSub.Native
{
    public delegate void NativeCallback(string info, string json);
    
    public interface INativeBridge
    {
        void InitNative(NativeCallback listener);
        void Send(string info, string json);
        string SendSync(string info, string json);
    }
}

 