using MiniSDK.PubSub;
using MiniSDK.PubSub.Data;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

public struct ToastData
{
    [JsonProperty(PropertyName = "toastMessage")]
    public string ToastMessage;

    [JsonProperty(PropertyName = "toastDuration")]
    public int ToastDuration;
}

public struct ToastResult
{
    [JsonProperty(PropertyName = "toastCount")]
    public string ToastCount;
}

public class SampleKit
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void __iOSSampleKitLoad();
#endif

    private Messenger messenger;
    
    // private Messenger testMessenger;

    public void Initialize()
    {
#if UNITY_ANDROID
        AndroidJavaClass loaderClass = new AndroidJavaClass("com.pj.sample.SampleKitLoader");
        loaderClass.CallStatic("load");
#elif UNITY_IOS
        __iOSSampleKitLoad(); 
#endif
        messenger = new Messenger();
        messenger.Subscribe("SEND_TOAST_RESULT", OnNative);
    }

    private void OnNative(Message message)
    {
        Debug.Log("[unity pubsubtest] message toast count : " + message.Data<ToastResult>().ToastCount);
    }

    public void CallTest()
    {
        messenger.Publish(new Topic{Key = "SEND_TOAST", Target = SdkType.Native}, new Payload(new ToastData{ToastDuration = 1, ToastMessage = "toast of unity"}));
    }

    public void AsyncCallTest()
    {
        Payload payload = new Payload(
            new ToastData { ToastDuration = 1, ToastMessage = "[unity] toast async call" });
        messenger.Publish(new Topic{Key = "SEND_TOAST_ASYNC", Target = SdkType.Native}, payload, receivedMessage =>
        {
            Debug.Log("[unity pubsubtest] received async payload toast count :  + " + receivedMessage.Data<ToastResult>().ToastCount);
        });
    }
}
