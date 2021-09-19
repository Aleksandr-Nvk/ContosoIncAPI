using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	public record ConcurrentLogin
	{
		[JsonProperty(PropertyName = "userName", Order = -1)]
		public string UserName {get; init;}
		
		[JsonProperty(PropertyName = "deviceName", Order = 0)]
		public string DeviceName {get; init;}
		
		[JsonIgnore]
		public DateTime LoginTs {get; init;}
		
		[JsonProperty(PropertyName = "loginTime", Order = 1)]
		public string LoginTsString => LoginTs.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
	}
}