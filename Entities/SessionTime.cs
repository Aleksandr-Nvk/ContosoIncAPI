using Newtonsoft.Json;
using System;

namespace ContosoIncAPI.Entities
{
	public record SessionTime
	{
		[JsonIgnore]
		public DateTime Date {get; set;}
		
		[JsonProperty(PropertyName = "hour")]
		public uint Hour {get; set;}
		
		[JsonProperty(PropertyName = "totalTimeForHour")]
		public uint Duration {get; set;}
		
		[JsonProperty(PropertyName = "qumulativeForHour")]
		public uint DurationAccumulated {get; set;}
	}
}