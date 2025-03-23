using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class Publisher : Node
    {
        private static readonly IdCounter IdCounter = new IdCounter();
        
        public void Publish(Topic topic, Payload payload)
        {
            NodeInfo nodeInfo = new NodeInfo() { MessageOwnerId = Id, PublisherId = Id };
            Message message = new Message(nodeInfo, topic, Topic.Default, payload);
            MessageManager.Instance.Mediator.Broadcast(message);
        }

        public void Publish(Topic topic, Payload payload, ReceiveDelegate responseCallback)
        {
            // Create reply topic
            string replyKey = $"{topic.Key}_id{IdCounter.GetNext()}";
            Topic replyTopic = new Topic { Key = replyKey, Target = SdkType.Game };
            // Register instant receiver
            Receiver receiver = new Receiver(-1, replyTopic.Key, replyTopic.Target, responseCallback);
            MessageManager.Instance.Mediator.RegisterInstantReceiver(receiver);
            
            // Broadcast message
            NodeInfo nodeInfo = new NodeInfo() { MessageOwnerId = Id, PublisherId = Id };
            Message message = new Message(nodeInfo, topic, replyTopic, payload);
            MessageManager.Instance.Mediator.Broadcast(message);
        }

        public void Reply(MessageInfo receivedMessageInfo, Payload payload)
        {
            this.Publish(receivedMessageInfo.ReplyTopic, payload);
        }
    }
    
}