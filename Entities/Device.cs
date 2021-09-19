using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{  
	// model for table 2
	public record Device
	{
		[JsonIgnore]
		public short Year {get; init;}
		
		[JsonIgnore]
		public string Month {get; init;}
		
		[JsonProperty(PropertyName = "type")]
		public string DeviceType {get; init;}
		
		[JsonProperty(PropertyName = "value")]
		public uint UsersNum {get; init;}
	}
}