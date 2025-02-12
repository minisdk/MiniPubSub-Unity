using Newtonsoft.Json;

namespace MiniSDK.PubSub.Data
{
    public struct NodeInfo
    {
        [JsonProperty(PropertyName = "requestOwnerId")]
        public int RequestOwnerId;

        [JsonProperty(PropertyName = "publisherId")]
        public int PublisherId;
    }
    
    public struct RequestInfo
    {
        [JsonProperty(PropertyName = "nodeInfo")]
        public NodeInfo NodeInfo;
        [JsonProperty(PropertyName = "key")]
        public string Key;
        [JsonProperty(PropertyName = "responseKey")]
        public string ResponseKey;

        [JsonIgnore]
        public bool IsResponsible => !string.IsNullOrEmpty(ResponseKey);
    }

    public struct ResponseInfo
    {
        public string Key;
    }
    
    public struct Request
    {
        [JsonProperty(PropertyName = "info")] 
        public RequestInfo Info;
        [JsonProperty(PropertyName = "json")]
        public string Json;

        [JsonIgnore]
        public string Key => Info.Key;
        
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(Json);
        }
        
        public Request(NodeInfo nodeInfo, string key, string json, string responseKey)
        {
            Info = new RequestInfo
            {
                NodeInfo = nodeInfo,
                Key = key, 
                ResponseKey = responseKey
            };
            Json = json;
        }

        internal Request(RequestInfo info, string json)
        {
            Info = info;
            Json = json;
        }

        public ResponseInfo GetResponseInfo()
        {
            return new ResponseInfo{Key = Info.ResponseKey};
        }
    }
}