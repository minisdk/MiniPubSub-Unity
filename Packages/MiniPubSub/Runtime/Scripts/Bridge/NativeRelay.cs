using MiniSDK.Core.Module;
using MiniSDK.PubSub;
using Newtonsoft.Json;
using MiniSDK.PubSub.Data;
using UnityEngine;

namespace MiniSDK.Native
{
    public class NativeRelay : ModuleBase
    {
        private INativeBridge bridge;
        private Watcher watcher;

        public override void Initialize()
        {
            base.Initialize();
            watcher = new Watcher();
            watcher.Watch(OnWatch);
            bridge = new NativeBridge();
            bridge.SetNativeCallbackListener(OnReceiveFromNative);
        }

        private void OnReceiveFromNative(string info, string json)
        {
            RequestInfo requestInfo = JsonConvert.DeserializeObject<RequestInfo>(info);
            NodeInfo nodeInfo = new NodeInfo { RequestOwnerId = requestInfo.NodeInfo.RequestOwnerId, PublisherId = watcher.Id };
            Request request = new Request
            (
                new RequestInfo
                {
                    NodeInfo = nodeInfo,
                    Key = requestInfo.Key,
                    ResponseKey = requestInfo.ResponseKey
                },
                json
            );
            MessageManager.Instance.Mediator.Broadcast(request);
        }

        private void OnWatch(Request request)
        {
            string info = JsonConvert.SerializeObject(request.Info);
            bridge.Send(info, request.Json);
        }

    }

}

