using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	public record UnseenCountryLogin
	{
		[JsonIgnore]
		public string UserName {get; init;}
		
		[JsonProperty(PropertyName = "country", Order = -1)]
		public string Country {get; init;}
		
		[JsonIgnore]
		public DateTime LoginTs {get; init;}
		
		[JsonProperty(PropertyName = "loginTime", Order = 0)]
		public string LoginTsString => LoginTs.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
	}
}