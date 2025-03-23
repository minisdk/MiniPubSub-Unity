using Newtonsoft.Json;

namespace MiniSDK.PubSub.Data
{
    public struct Topic
    {
        public static Topic Default = new Topic{Key = "", Target = SdkType.Game};
        
        [JsonProperty(PropertyName = "key")]
        public string Key;
        [JsonProperty(PropertyName = "target")]
        public SdkType Target;
    }
}