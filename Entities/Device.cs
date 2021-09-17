using System;

namespace ContosoIncAPI.Entities
{  
    [Serializable]
    public record Device
    {
        public string type {get; set;}
        public uint value {get; set;}
    }
}