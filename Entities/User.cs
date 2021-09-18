using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
    public record User
    {
        [JsonProperty(PropertyName = "year")]
        public short Year {get; set;}
        
        [JsonProperty(PropertyName = "month")]
        public string Month {get; set;}
        
        [JsonProperty(PropertyName = "registeredUsers")]
        public uint UsersNum {get; set;}
    }
}