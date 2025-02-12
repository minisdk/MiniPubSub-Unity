using System.Collections.Concurrent;
using System.Collections.Generic;
using MiniSDK.PubSub.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace MiniSDK.PubSub
{
    internal class MessageMediatorImpl : MessageMediator
    {
        private static string WatcherKey => "Key_Watcher_Reserved";
        private readonly ConcurrentDictionary<string, List<Receiver>> receiverDic = new ConcurrentDictionary<string, List<Receiver>>();
        private readonly ConcurrentDictionary<string, Receiver> instantReceiverDic = new ConcurrentDictionary<string, Receiver>();
        
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

        public void Broadcast(Request request)
        {
            if (instantReceiverDic.TryRemove(request.Key, out var instantReceiver))
            {
                instantReceiver.ReceiverDelegate?.Invoke(request);
            }
            
            if (receiverDic.TryGetValue(request.Key, out var receivers))
            {
                foreach (var receiver in receivers)
                {
                    if(receiver.NodeId == request.Info.NodeInfo.PublisherId) 
                        continue;
                    receiver.ReceiverDelegate?.Invoke(request);
                }
            }

            if (receiverDic.TryGetValue(WatcherKey, out var watchers))
            {
                foreach (var watcher in watchers)
                {
                    if(watcher.NodeId == request.Info.NodeInfo.PublisherId) 
                        continue;
                    watcher.ReceiverDelegate?.Invoke(request);
                }
            }
        }

    }
}