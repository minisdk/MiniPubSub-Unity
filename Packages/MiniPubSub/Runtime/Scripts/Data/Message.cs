using Newtonsoft.Json;

namespace MiniSDK.PubSub.Data
{
    public struct NodeInfo
    {
        [JsonProperty(PropertyName = "messageOwnerId")]
        public int MessageOwnerId;

        [JsonProperty(PropertyName = "publisherId")]
        public int PublisherId;
    }
    
    public struct MessageInfo
    {
        [JsonProperty(PropertyName = "nodeInfo")]
        public NodeInfo NodeInfo;
        [JsonProperty(PropertyName = "key")]
        public string Key;
        [JsonProperty(PropertyName = "replyKey")]
        public string ReplyKey;
    }
    
    public struct Message
    {
        public MessageInfo Info;
        public Payload Payload;

        [JsonIgnore]
        public string Key => Info.Key;
        
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(Payload.Json);
        }
        
        public Message(NodeInfo nodeInfo, string key, Payload payload, string replyKey)
        {
            Info = new MessageInfo
            {
                NodeInfo = nodeInfo,
                Key = key, 
                ReplyKey = replyKey
            };
            Payload = payload;
        }

        internal Message(MessageInfo info, Payload payload)
        {
            Info = info;
            Payload = payload;
        }
    }
}