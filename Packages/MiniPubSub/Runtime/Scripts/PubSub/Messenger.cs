using System;
using System.Collections.Generic;
using MiniSDK.PubSub.Data;


namespace MiniSDK.PubSub
{
    public sealed class Messenger : Publisher
    {
        public void Subscribe(string key, ReceiveDelegate receiveDelegate)
        {
            Receiver receiver = new Receiver(this.Id, key, receiveDelegate);
            MessageManager.Instance.Mediator.Register(receiver);
        }

        public void Unsubscribe(string key)
        {
            MessageManager.Instance.Mediator.Unregister(Id, key);   
        }

    }
}