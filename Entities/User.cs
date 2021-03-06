using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
    // model for table 1
    public record User
    {
        [JsonProperty(PropertyName = "year")]
        public short Year {get; init;}
        
        [JsonProperty(PropertyName = "month")]
        public string Month {get; init;}
        
        [JsonProperty(PropertyName = "registeredUsers")]
        public uint UsersNum {get; init;}
    }
}