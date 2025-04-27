using System;
using System.Collections.Generic;
using MiniSDK.PubSub.Data;


namespace MiniSDK.PubSub
{
    public sealed class Messenger : Publisher
    {
        private readonly SdkType target;

        public Messenger(SdkType target = SdkType.Game)
        {
            this.target = target;
        }
        
        public void Subscribe(string key, ReceiveDelegate receiveDelegate)
        {
            Receiver receiver = new Receiver(this.Id, key, target, receiveDelegate);
            MessageManager.Instance.Mediator.Register(receiver);
        }

        public void Handle(string key , HandleDelegate handleDelegate)
        {
            Handler handler = new Handler(this.Id, key, target, handleDelegate);
            MessageManager.Instance.Mediator.Handle(handler);
        }

        public void Unsubscribe(string key)
        {
            MessageManager.Instance.Mediator.Unregister(Id, key);   
        }

    }
}