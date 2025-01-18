using MiniSDK.Core.Module;
using MiniSDK.PubSub;
using Newtonsoft.Json;
using MiniSDK.PubSub.Data;

namespace MiniSDK.Native
{
    public class NativeRelay : ModuleBase
    {
        private INativeBridge bridge;
        private Watcher watcher;

        public override void Initialize()
        {
            base.Initialize();
            watcher = new Watcher();
            watcher.Watch(OnWatch);
            bridge = new NativeBridge();
            bridge.SetNativeCallbackListener(OnReceiveFromNative);
        }

        private void OnReceiveFromNative(string info, string json)
        {
            Message message = new Message
            {
                Info = JsonConvert.DeserializeObject<MessageInfo>(info), 
                Json = json
            };
        
            watcher.Publish(message);
        }

        private void OnWatch(Message message)
        {
            string info = JsonConvert.SerializeObject(message.Info);
            bridge.Send(info, message.Json);
        }

    }

}

