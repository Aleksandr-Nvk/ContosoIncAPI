using System;
using System.Collections.Generic;

namespace ContosoIncAPI.Entities
{
    [Serializable]
    public record User
    {
        public short year {get; set;}
        public string month {get; set;}
        public uint registeredUsers {get; set;}

        public IEnumerable<Device> registeredDevices {get; set;}
    }
}