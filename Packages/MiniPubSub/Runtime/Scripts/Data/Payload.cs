using Newtonsoft.Json;
using UnityEngine;

namespace MiniSDK.PubSub.Data
{
    public class Payload
    {
        public string Json;
        
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(Json);
        }

        public Payload(string json)
        {
            Json = json;
        }

        public Payload(object data)
        {
            Json = JsonConvert.SerializeObject(data);
        }
    }
    
}
