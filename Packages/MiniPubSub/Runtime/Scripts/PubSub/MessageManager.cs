using System;

using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class MessageManager
    {
        public static MessageManager Instance { get; private set; } = new MessageManager();

        private MessageManager() { }

        private readonly Lazy<MessageMediator> lazyMediator = new Lazy<MessageMediator>(()=> new MessageMediatorImpl());
        internal MessageMediator Mediator => lazyMediator.Value;
    }
}