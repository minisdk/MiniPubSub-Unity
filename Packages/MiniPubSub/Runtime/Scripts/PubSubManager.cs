using MiniSDK.PubSub.Data;
using MiniSDK.PubSub.Native;

namespace MiniSDK.PubSub
{
    public class PubSubManager
    {
        public static PubSubManager Instance { get; private set; } = new PubSubManager();

        private NativeRelay relay;

        private PubSubManager()
        {
            relay = new NativeRelay();
            relay.Initialize();
        }

        public Messenger CreateMessenger(SdkType type = SdkType.Native)
        {
            return new Messenger(type);
        }

        public Watcher CreateWatcher(SdkType type = SdkType.Native)
        {
            return new Watcher(type);
        }

        public Publisher CreatePublisher()
        {
            return new Publisher();
        }

    }
}