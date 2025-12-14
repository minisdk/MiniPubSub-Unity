namespace MiniSDK.PubSub.ModuleHelper
{
    public class ModuleHelper 
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        private static IModuleLoader moduleLoader = new AndroidModuleLoader();
#elif !UNITY_EDITOR && UNITY_IOS
        private static IModuleLoader moduleLoader = new IOSModuleLoader();
#else
        private static IModuleLoader moduleLoader = null;
#endif
        public static void LoadPlugin(string className) 
        {
            moduleLoader?.LoadModule(className);
        }
    }
}