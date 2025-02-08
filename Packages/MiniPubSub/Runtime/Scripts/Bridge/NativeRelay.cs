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
            Request request = new Request
            (
                JsonConvert.DeserializeObject<RequestInfo>(info), 
                json
            );
            MessageManager.Instance.Mediator.Broadcast(request);
        }

        private void OnWatch(Request request)
        {
            string info = JsonConvert.SerializeObject(request.Info);
            bridge.Send(info, request.Json);
        }

    }

}

