

using MiniSDK.PubSub.Data;

namespace MiniSDK.PubSub
{
    public abstract class Node
    {
        #region IdGenerator
        private static int _nodeIdGen = (int) SdkType.Game;
        #endregion
        public int Id { get; } = SdkUtil.IssueID(ref _nodeIdGen);
    }    
}