
namespace MiniSDK.Native
{
    public class NativeBridge : INativeBridge
    {
        private INativeBridge bridge;

        public NativeBridge()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            bridge = new AndroidBridge();
#elif UNITY_IOS && !UNITY_EDITOR
            bridge = new IOSBridge();
#else
            bridge = new EditorBridge();
#endif 
        }

        public void InitNative(NativeCallback listener)
        {
            bridge?.InitNative(listener);
        }

        public void Send(string info, string json)
        {
            bridge?.Send(info, json);
        }

        public string SendSync(string info, string json)
        {
            return bridge?.SendSync(info, json);
        }
    }
}
