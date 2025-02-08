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
    
    // private Messenger testMessenger;

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
        // testMessenger = new Messenger();
        // testMessenger.Subscribe("SEND_TOAST_ASYNC", request =>
        // {
        //     Debug.Log("[unity pubsubtest] response test...");
        //     Request response = request.CreateResponse(new Message(new ToastResult { ToastCount = "999" }));
        //     MessageManager.Respond(response);
        // });
    }

    private void OnNative(Request request)
    {
        Debug.Log("[unity pubsubtest] message toast count : " + request.Data<ToastResult>().ToastCount);
    }

    public void CallTest()
    {
        messenger.Publish("SEND_TOAST", new Message(new ToastData{ToastDuration = 1, ToastMessage = "toast of unity"}));
    }

    public void AsyncCallTest()
    {
        Message message = new Message(
            new ToastData { ToastDuration = 1, ToastMessage = "[unity] toast async call" });
        messenger.Publish("SEND_TOAST_ASYNC", message, receivedMessage =>
        {
            Debug.Log("[unity pubsubtest] received async message toast count :  + " + receivedMessage.Data<ToastResult>().ToastCount);
        });
    }
}
