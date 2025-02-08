using System;

using MiniSDK.Core.Util;
using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public class MessageManager : Singleton<MessageManager>
    {
        private readonly Lazy<MessageMediator> lazyMediator = new Lazy<MessageMediator>(()=> new MessageMediatorImpl());
        internal MessageMediator Mediator => lazyMediator.Value;

        public static void Respond(Request response)
        {
            Instance.Mediator.Broadcast(response);
        }
    }
}