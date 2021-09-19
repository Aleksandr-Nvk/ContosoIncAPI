using Newtonsoft.Json;
using System;

namespace ContosoIncAPI.Entities
{  
    // model for table 3
    public record SessionCount
    {
        [JsonIgnore]
        public DateTime Date {get; init;}
        
        [JsonProperty(PropertyName = "concurrentSessions", Order = 1)]
        public uint SessionsNum {get; init;}
    }
}