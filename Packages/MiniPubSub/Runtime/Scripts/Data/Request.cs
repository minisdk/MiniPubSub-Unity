using Newtonsoft.Json;

namespace MiniSDK.PubSub.Data
{
    public struct RequestInfo
    {
        [JsonProperty(PropertyName = "requestOwnerId")]
        public int RequestOwnerId;
        [JsonProperty(PropertyName = "key")]
        public string Key;
        [JsonProperty(PropertyName = "responseKey")]
        public string ResponseKey;

        public bool IsResponsible => string.IsNullOrEmpty(ResponseKey);
    }
    
    public struct Request
    {
        [JsonProperty(PropertyName = "info")] 
        public RequestInfo Info;
        [JsonProperty(PropertyName = "json")]
        public string Json;

        public string Key => Info.Key;
        
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(Json);
        }
        
        public Request(string key, string json, int requestOwnerId, string responseKey)
        {
            Info = new RequestInfo
            {
                Key = key, 
                RequestOwnerId = requestOwnerId,
                ResponseKey = responseKey
            };
            Json = json;
        }

        internal Request(RequestInfo info, string json)
        {
            Info = info;
            Json = json;
        }

        public Request CreateResponse(Message message)
        {
            Request request = new Request(Info.ResponseKey, message.Json, -1, "");
            return request;
        }
    }
}