using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MiniSDK.Core.Module;
using MiniSDK.PubSub;
using MiniSDK.PubSub.Data;
using Newtonsoft.Json;
using UnityEngine;


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

public class SampleKit : ModuleBase
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void __iOSSampleKitLoad();
#endif

    private Messenger messenger;

    public override void Initialize()
    {
        base.Initialize();
#if UNITY_ANDROID
        AndroidJavaObject loaderObject = new AndroidJavaObject("com.pj.sample.SampleKitLoader");
        loaderObject.Call<string>("loadModule");
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
        messenger.Publish(new Message("SEND_TOAST", new ToastData{ToastDuration = 1, ToastMessage = "toast of unity"}));
    }
}
