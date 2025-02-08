using System.Threading;
using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class Publisher : Node
    {
        private static int _responseIdGen = (int)SdkType.Game; 
        
        public void Publish(string key, Message message)
        {
            Request request = new Request(key, message.Json, -1, "");
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
            Request request = new Request(key, message.Json, Id, responseKey);
            MessageManager.Instance.Mediator.Broadcast(request);
        }
    }
    
}