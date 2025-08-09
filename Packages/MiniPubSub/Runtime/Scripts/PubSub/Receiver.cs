using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public delegate void ReceiveDelegate(Message message);
    public class Receiver
    {
        public readonly int NodeId;
        public readonly string Key;
        public readonly ReceiveDelegate ReceiverDelegate;
        public readonly SdkType Target;

        public bool CanInvoke(MessageInfo info)
        {
            return NodeId != info.NodeInfo.PublisherId && Target == info.Topic.Target;
        }
        
        public Receiver(int id, string key, SdkType target, ReceiveDelegate receiverDelegate)
        {
            NodeId = id;
            Key = key;
            Target = target;
            ReceiverDelegate = receiverDelegate;
        }
    }
}