using Newtonsoft.Json;
using System;

namespace ContosoIncAPI.Entities
{  
    public record SessionCount
    {
        [JsonIgnore]
        public DateTime Date {get; set;}
        
        [JsonProperty(PropertyName = "concurrentSessions", Order = 1)]
        public uint SessionsNum {get; set;}
    }
}