#if UNITY_EDITOR || UNITY_ANDROID
using UnityEngine;

namespace MiniSDK.PubSub.ModuleHelper
{
    public class AndroidModuleLoader : IModuleLoader
    {
        private AndroidJavaClass moduleLoaderClass;

        public AndroidModuleLoader()
        {
            moduleLoaderClass = new AndroidJavaClass("com.minisdk.pubsub.module.AndroidModuleLoader");
        }
        public void LoadModule(string className)
        {
            moduleLoaderClass.CallStatic("load", className);
        }
    }
}
#endif