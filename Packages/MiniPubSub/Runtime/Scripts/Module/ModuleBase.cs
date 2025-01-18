namespace MiniSDK.Core.Module
{
    public abstract class ModuleBase
    {
        public bool IsInitialized { get; private set; }

        public virtual void Initialize()
        {
            IsInitialized = true;
        }
    }
}