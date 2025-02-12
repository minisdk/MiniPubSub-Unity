using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class Watcher : Node
    {
        private static string WatcherKey => "Key_Watcher_Reserved";
        public void Watch(ReceiveDelegate receiverDelegate)
        {
            Receiver receiver = new Receiver(Id, WatcherKey, receiverDelegate);
            MessageManager.Instance.Mediator.Register(receiver);
        }

        public void Unwatch()
        {
            MessageManager.Instance.Mediator.Unregister(Id, WatcherKey);
        }

    }
}