using System;

namespace MiniSDK.Core.Util
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        public static T Instance => lazyInstance.Value;
        
        private static Lazy<T> lazyInstance = new Lazy<T>(CreateInstance);

        private static T CreateInstance()
        {
            T created = new T();
            created.Initialize();
            return created;
        }

        protected virtual void Initialize() { }
    }

}

