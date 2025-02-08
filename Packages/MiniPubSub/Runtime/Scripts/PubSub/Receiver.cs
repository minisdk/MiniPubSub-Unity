using MiniSDK.PubSub.Data;


namespace MiniSDK.PubSub
{
    public delegate void ReceiveDelegate(Request request);
    public class Receiver
    {
        public readonly int NodeId;
        public readonly string Key;
        public readonly ReceiveDelegate ReceiverDelegate;
        
        public Receiver(int id, string key, ReceiveDelegate receiverDelegate)
        {
            NodeId = id;
            Key = key;
            ReceiverDelegate = receiverDelegate;
        }
    }
}