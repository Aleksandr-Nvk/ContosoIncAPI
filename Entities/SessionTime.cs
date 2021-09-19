using Newtonsoft.Json;
using System;

namespace ContosoIncAPI.Entities
{
	public record SessionTime
	{
		[JsonIgnore]
		public DateTime Date {get; init;}
		
		[JsonProperty(PropertyName = "hour")]
		public uint Hour {get; init;}
		
		[JsonProperty(PropertyName = "totalTimeForHour")]
		public uint Duration {get; init;}
		
		[JsonProperty(PropertyName = "qumulativeForHour")]
		public uint DurationAccumulated {get; init;}
	}
}