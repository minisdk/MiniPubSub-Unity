using System.Collections.Concurrent;
using System.Collections.Generic;
using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    internal class MessageMediatorImpl : MessageMediator
    {
        private static string WatcherKey => "Key_Watcher_Reserved";
        private readonly ConcurrentDictionary<string, List<Receiver>> receiverDic = new ConcurrentDictionary<string, List<Receiver>>();
        private readonly ConcurrentDictionary<string, Receiver> instantReceiverDic = new ConcurrentDictionary<string, Receiver>();
        private readonly ConcurrentDictionary<string, Handler> handlerDic = new ConcurrentDictionary<string, Handler>();
        private readonly ConcurrentDictionary<SdkType, Handler> targetHandlerDic = new ConcurrentDictionary<SdkType, Handler>();
        
        public void Register(string key, Receiver receiver)
        {
            if (receiverDic.ContainsKey(key))
            {
                receiverDic[key].Add(receiver);
            }
            else
            {
                receiverDic[key] = new List<Receiver> {receiver};
            }
        }

        public void Register(Receiver receiver)
        {
            string key = receiver.Key;
            if (receiverDic.ContainsKey(key))
            {
                receiverDic[key].Add(receiver);
            }
            else
            {
                receiverDic[key] = new List<Receiver> {receiver};
            }
        }

        public void Unregister(int id, string key)
        {
            if (receiverDic.TryGetValue(key, out List<Receiver> receivers))
            {
                receivers.RemoveAll(receiver => receiver.NodeId == id);
            }
        }
        
        public void RegisterInstantReceiver(Receiver receiver)
        {
            instantReceiverDic[receiver.Key] = receiver;
        }

        public void Broadcast(Message message)
        {
            if (instantReceiverDic.TryRemove(message.Key, out var instantReceiver))
            {
                instantReceiver.ReceiverDelegate?.Invoke(message);
            }
            
            if (receiverDic.TryGetValue(message.Key, out var receivers))
            {
                foreach (var receiver in receivers)
                {
                    if (receiver.CanInvoke(message.Info))
                    {
                        receiver.ReceiverDelegate?.Invoke(message);
                    }
                }
            }

            if (receiverDic.TryGetValue(WatcherKey, out var watchers))
            {
                foreach (var watcher in watchers)
                {
                    if (watcher.CanInvoke(message.Info))
                    {
                        watcher.ReceiverDelegate?.Invoke(message);
                    }
                }
            }
        }

        public void Handle(Handler handler)
        {
            handlerDic[handler.Key] = handler;
        }

        public void HandleTarget(Handler handler)
        {
            targetHandlerDic[handler.Target] = handler;
        }

        public Payload SendSync(Message message)
        {
            if (handlerDic.TryGetValue(message.Key, out Handler handler))
            {
                if (handler.CanInvoke(message.Info))
                {
                    return handler.HandleDelegate?.Invoke(message);
                }
                return new Payload("{}");
            }

            if (targetHandlerDic.TryGetValue(message.Info.Topic.Target, out handler))
            {
                if (handler.CanInvoke(message.Info))
                {
                    return handler.HandleDelegate?.Invoke(message);
                }
                return new Payload("{}");
            }

            return new Payload("{}");
        }
    }
}