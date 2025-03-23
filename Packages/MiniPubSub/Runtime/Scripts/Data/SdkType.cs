
namespace MiniSDK.PubSub.Data
{
    // TODO: Platform 이라고 하지 말고 layer? 게임 엔진단과 네이티브단을 구분할만한 이름으로 
    public enum SdkType
    {
        Native        = 0,
        Game          = 1
    }

    public class IdCounter
    {
        private readonly object idLock = new object();
        private int idSource = (int)SdkType.Game;
        public int GetNext()
        {
            lock (idLock)
            {
                int id = idSource;
                idSource += 2; // number of PlatformTypes
                return id;    
            }
        }
    }
}