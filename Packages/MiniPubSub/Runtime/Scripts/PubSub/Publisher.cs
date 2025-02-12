using System.Threading;
using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class Publisher : Node
    {
        private static int _responseIdGen = (int)SdkType.Game; 
        
        public void Publish(string key, Message message)
        {
            NodeInfo nodeInfo = new NodeInfo() { RequestOwnerId = Id, PublisherId = Id };
            Request request = new Request(nodeInfo, key, message.Json, "");
            MessageManager.Instance.Mediator.Broadcast(request);
        }

        public void Publish(string key, Message message, ReceiveDelegate responseCallback)
        {
            // Create responseKey
            string responseKey = $"{key}_id{SdkUtil.IssueID(ref _responseIdGen)}";
            // Register instant receiver
            Receiver receiver = new Receiver(-1, responseKey, responseCallback);
            MessageManager.Instance.Mediator.RegisterInstantReceiver(receiver);
            // Broadcast request
            NodeInfo nodeInfo = new NodeInfo() { RequestOwnerId = Id, PublisherId = Id };
            Request request = new Request(nodeInfo, key, message.Json, responseKey);
            MessageManager.Instance.Mediator.Broadcast(request);
        }

        public void Respond(ResponseInfo responseInfo, Message message)
        {
            Request request = new Request(new NodeInfo
            {
                PublisherId = Id,
                RequestOwnerId = Id
            }, responseInfo.Key, message.Json, "");
            MessageManager.Instance.Mediator.Broadcast(request);
        }
    }
    
}