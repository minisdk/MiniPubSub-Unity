using System.Threading;
using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class Publisher : Node
    {
        private static readonly IdCounter IdCounter = new IdCounter();
        
        public void Publish(string key, Payload payload)
        {
            NodeInfo nodeInfo = new NodeInfo() { MessageOwnerId = Id, PublisherId = Id };
            Message message = new Message(nodeInfo, key, payload, "");
            MessageManager.Instance.Mediator.Broadcast(message);
        }

        public void Publish(string key, Payload payload, ReceiveDelegate responseCallback)
        {
            // Create replyKey
            string replyKey = $"{key}_id{IdCounter.GetNext()}";
            // Register instant receiver
            Receiver receiver = new Receiver(-1, replyKey, responseCallback);
            MessageManager.Instance.Mediator.RegisterInstantReceiver(receiver);
            // Broadcast message
            NodeInfo nodeInfo = new NodeInfo() { MessageOwnerId = Id, PublisherId = Id };
            Message message = new Message(nodeInfo, key, payload, replyKey);
            MessageManager.Instance.Mediator.Broadcast(message);
        }

        public void Reply(MessageInfo receivedMessageInfo, Payload payload)
        {
            Message message = new Message(new NodeInfo
            {
                PublisherId = Id,
                MessageOwnerId = Id
            }, receivedMessageInfo.ReplyKey, payload, "");
            MessageManager.Instance.Mediator.Broadcast(message);
        }
    }
    
}