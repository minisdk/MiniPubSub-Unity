using MiniSDK.PubSub;
using Newtonsoft.Json;
using MiniSDK.PubSub.Data;
using UnityEngine;

namespace MiniSDK.Native
{
    public class NativeRelay
    {
        private INativeBridge bridge;
        private Watcher watcher;

        public void Initialize()
        {
            watcher = new Watcher();
            watcher.Watch(OnWatch);
            bridge = new NativeBridge();
            bridge.SetNativeCallbackListener(OnReceiveFromNative);
        }

        private void OnReceiveFromNative(string info, string json)
        {
            MessageInfo messageInfo = JsonConvert.DeserializeObject<MessageInfo>(info);
            NodeInfo nodeInfo = new NodeInfo { MessageOwnerId = messageInfo.NodeInfo.MessageOwnerId, PublisherId = watcher.Id };
            Message message = new Message
            (
                new MessageInfo
                {
                    NodeInfo = nodeInfo,
                    Key = messageInfo.Key,
                    ReplyKey = messageInfo.ReplyKey
                },
                new Payload(json)
            );
            MessageManager.Instance.Mediator.Broadcast(message);
        }

        private void OnWatch(Message message)
        {
            string info = JsonConvert.SerializeObject(message.Info);
            bridge.Send(info, message.Payload.Json);
        }

    }

}

