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
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic;
        [JsonProperty(PropertyName = "replyTopic")]
        public Topic ReplyTopic;
    }
    
    public struct Message
    {
        public MessageInfo Info;
        public readonly Payload Payload;

        [JsonIgnore]
        public string Key => Info.Topic.Key;
        
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(Payload.Json);
        }
        
        public Message(NodeInfo nodeInfo, Topic topic, Topic replyTopic, Payload payload)
        {
            Info = new MessageInfo
            {
                NodeInfo = nodeInfo,
                Topic = topic, 
                ReplyTopic = replyTopic
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