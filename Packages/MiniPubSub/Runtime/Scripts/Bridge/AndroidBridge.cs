#if UNITY_ANDROID
using System;
using UnityEngine;

namespace MiniSDK.Native
{
    public class AndroidBridgeProxy : AndroidJavaProxy
    {
        public event NativeCallback NativeCallback;

        public AndroidBridgeProxy() : base("com.minisdk.pubsub.bridge.NativeBridgeCallback")
        {
            
        }

        public void onReceive(string info, string json)
        {
            NativeCallback?.Invoke(info, json);
        }
    }

    public class AndroidBridge : INativeBridge 
    {
        private AndroidBridgeProxy androidBridgeProxy;

        private Lazy<AndroidJavaObject> androidBridge = new Lazy<AndroidJavaObject>(()=>
        {
            AndroidJavaObject obj = new AndroidJavaObject("com.minisdk.pubsub.bridge.UnityNativeBridge");
            return obj;
        });

        public AndroidBridge()
        {
            // unity activity
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            
            androidBridgeProxy = new AndroidBridgeProxy();
            androidBridge.Value.Call("initialize", currentActivity, androidBridgeProxy);
        }

        public void SetNativeCallbackListener(NativeCallback listener)
        {
            androidBridgeProxy.NativeCallback -= listener;
            androidBridgeProxy.NativeCallback += listener;
        }

        public void Send(string info, string json)
        {
            androidBridge.Value.Call("send", info, json);
        }
    }
    
}

#endif