using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	public record SessionCountTime : SessionCount
	{
		[JsonProperty(PropertyName = "date", Order = -1)]
		public new string Date {get; init;}
		
		[JsonProperty(PropertyName = "hour", Order = 0)]
		public uint Hour {get; init;}
		
		[JsonProperty(PropertyName = "totalTimeForHour", Order = 2)]
		public uint Duration {get; init;}
		
		[JsonProperty(PropertyName = "qumulativeForHour", Order = 3)]
		public uint DurationAccumulated {get; init;}
	}
}