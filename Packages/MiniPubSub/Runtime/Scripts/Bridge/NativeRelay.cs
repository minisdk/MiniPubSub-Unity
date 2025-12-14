using Newtonsoft.Json;
using MiniSDK.PubSub.Data;
using UnityEngine;

namespace MiniSDK.PubSub.Native
{
    public class NativeRelay
    {
        private INativeBridge bridge;
        private Watcher watcher;

        public void Initialize()
        {
            watcher = new Watcher(SdkType.Native);
            watcher.Watch(OnWatch);

            Handler handler = new Handler(watcher.Id, "", SdkType.Native, OnHandle);
            MessageManager.Instance.Mediator.Handle(handler.Target, handler);
            
            bridge = new NativeBridge();
            bridge.InitNative(OnReceiveFromNative);
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
                    Topic = messageInfo.Topic,
                    ReplyTopic = messageInfo.ReplyTopic
                },
                new Payload(json)
            );
            MessageManager.Instance.Mediator.Broadcast(message);
        }

        private Payload OnHandle(Message message)
        {
            string info = JsonConvert.SerializeObject(message.Info);
            string result = bridge.SendSync(info, message.Payload.Json);
            return new Payload(result);
        }

        private void OnWatch(Message message)
        {
            string info = JsonConvert.SerializeObject(message.Info);
            bridge.Send(info, message.Payload.Json);
        }

    }

}

