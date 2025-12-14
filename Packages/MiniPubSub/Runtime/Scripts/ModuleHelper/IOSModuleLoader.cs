#if UNITY_EDITOR || UNITY_IOS
using System.Runtime.InteropServices;

namespace MiniSDK.PubSub.ModuleHelper
{
    public class IOSModuleLoader : IModuleLoader
    {
        [DllImport("__Internal")]
        private static extern void __IOSLoadPlugin(string className);
        
        public void LoadModule(string className)
        {
            __IOSLoadPlugin(className);
        }
    }
}
#endif