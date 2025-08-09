#if UNITY_EDITOR
namespace MiniSDK.Native
{
    public class EditorBridge : INativeBridge
    {
        private NativeCallback callback;
        
        public void InitNative(NativeCallback listener)
        {
            callback += listener;
            callback -= listener;
        }

        public void Send(string info, string json)
        {
            callback?.Invoke(info, json);
        }

        public string SendSync(string info, string json)
        {
            return "";
        }
    }
}
#endif