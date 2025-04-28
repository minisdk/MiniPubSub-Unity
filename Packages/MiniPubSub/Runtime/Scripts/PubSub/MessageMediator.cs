using System.Collections;
using System.Collections.Generic;
using MiniSDK.PubSub.Data;


namespace MiniSDK.PubSub
{
    internal interface MessageMediator
    {
        void Register(Receiver receiver);
        void Unregister(int id, string key);
        void RegisterInstantReceiver(Receiver receiver);
        void Broadcast(Message message);
        void Handle(string key, Handler handler);
        void Handle(SdkType target, Handler handler);
        Payload SendSync(Message message);
    }

}
