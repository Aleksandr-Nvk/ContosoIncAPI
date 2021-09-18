using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	public record SessionCountTime : SessionCount
	{
		[JsonProperty(PropertyName = "date", Order = -1)]
		public new string Date {get; set;}
		
		[JsonProperty(PropertyName = "hour", Order = 0)]
		public uint Hour {get; set;}
		
		[JsonProperty(PropertyName = "totalTimeForHour", Order = 2)]
		public uint Duration {get; set;}
		
		[JsonProperty(PropertyName = "qumulativeForHour", Order = 3)]
		public uint DurationAccumulated {get; set;}
	}
}