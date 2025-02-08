using Newtonsoft.Json;
using UnityEngine;

namespace MiniSDK.PubSub.Data
{
    public class Message
    {
        public string Json;
        
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(Json);
        }
        
        public Message(object data)
        {
            Json = JsonConvert.SerializeObject(data);
        }
    }
    
}
