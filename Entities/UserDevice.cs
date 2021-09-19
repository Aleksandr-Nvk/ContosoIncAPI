using System.Collections.Generic;
using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	// compound model for task 1
	public record UserDevice : User
	{
		[JsonProperty(PropertyName = "registeredDevices", Order = 0)]
		public IEnumerable<Device> RegisteredDevices {get; init;}
	}
}