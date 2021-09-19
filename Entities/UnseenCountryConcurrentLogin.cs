using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	// compound model for task 3
	public record UnseenCountryConcurrentLogin : ConcurrentLogin
	{
		[JsonProperty(PropertyName = "unexpectedLogin", Order = 2)]
		public UnseenCountryLogin UnseenCountryLogin {get; init;}
	}
}