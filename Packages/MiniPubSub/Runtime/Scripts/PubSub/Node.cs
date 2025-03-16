

using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public abstract class Node
    {
        #region IdGenerator

        private static readonly IdCounter IdCounter = new IdCounter();
        #endregion
        public int Id { get; } = IdCounter.GetNext();
    }    
}