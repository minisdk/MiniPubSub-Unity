using UnityEngine;

namespace MiniSDK.Native
{
    public static class Initializer
    {
        private static NativeRelay _nativeRelay;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Initialize()
        {
            _nativeRelay = new NativeRelay();
            _nativeRelay.Initialize();
        }
    }
}