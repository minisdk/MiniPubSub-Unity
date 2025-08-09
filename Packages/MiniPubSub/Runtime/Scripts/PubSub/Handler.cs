using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public delegate Payload HandleDelegate(Message message);
    public class Handler
    {
        public readonly int NodeId;
        public readonly string Key;
        public readonly HandleDelegate HandleDelegate;
        public readonly SdkType Target;
        
        public bool CanInvoke(MessageInfo info)
        {
            return NodeId != info.NodeInfo.PublisherId && Target == info.Topic.Target;
        }
        
        public Handler(int id, string key, SdkType target, HandleDelegate handleDelegate)
        {
            NodeId = id;
            Key = key;
            Target = target;
            HandleDelegate = handleDelegate;
        }
    }
}