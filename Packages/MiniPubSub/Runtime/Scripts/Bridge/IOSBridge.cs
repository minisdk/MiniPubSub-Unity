#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;

namespace MiniSDK.Native
{
    public class IOSBridge : INativeBridge
    {
        // private delegate void ObjcStringDelegate(string data);
        private static event NativeCallback IOSNativeCallback;
        
        [MonoPInvokeCallback(typeof(NativeCallback))]
        private static void OnIOSStringEvent(string info, string json)
        {
            IOSNativeCallback?.Invoke(info, json);
        }
        
        [DllImport("__Internal")]
        private static extern void __iOSInitialize(NativeCallback stringDel);
        [DllImport("__Internal")]
        private static extern void __iOSSend(string info, string data);

        [DllImport("__Internal")]
        private static extern string __iOSSendSync(string info, string data);

        public IOSBridge()
        {
            __iOSInitialize(OnIOSStringEvent);
        }

        public void InitNative(NativeCallback listener)
        {
            IOSNativeCallback -= listener;
            IOSNativeCallback += listener;
        }

        public void Send(string info, string json)
        {
            __iOSSend(info, json);
        }

        public string SendSync(string info, string json)
        {
            return __iOSSendSync(info, json);
        }
    }
}

#endif