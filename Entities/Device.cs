using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{  
	public record Device
	{
		[JsonIgnore]
		public short Year {get; set;}
		
		[JsonIgnore]
		public string Month {get; set;}
		
		[JsonProperty(PropertyName = "type")]
		public string DeviceType {get; set;}
		
		[JsonProperty(PropertyName = "value")]
		public uint UsersNum {get; set;}
	}
}