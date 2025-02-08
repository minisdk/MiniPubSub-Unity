using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class Watcher : Node
    {
        private static string WatcherKey => "Key_Watcher_Reserved";
        public void Watch(ReceiveDelegate receiverDelegate)
        {
            Receiver receiver = new Receiver(Id, WatcherKey, receiverDelegate);
            // TODO : Register 활용 가능할 듯, Watcher용 키는 Watcher끼리 공유하고
            MessageManager.Instance.Mediator.Register(receiver);
        }

        public void Unwatch()
        {
            MessageManager.Instance.Mediator.Unregister(Id, WatcherKey);
        }

    }
}